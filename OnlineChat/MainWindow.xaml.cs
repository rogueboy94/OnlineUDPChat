using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace OnlineChat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SynchronizationContext _context; // для проброса кода из одного потока в другой

        bool alive = false; // будет ли работать поток для приема
        UdpClient client; 
        int PORT = Properties.Settings.Default.Port; /// порт для приема сообщений
        int TTL = Properties.Settings.Default.TTL; /// Время жизни пакета данных
        string HOST = Properties.Settings.Default.Host; /// хост для групповой рассылки
        IPAddress groupAddress; // адрес для групповой рассылки
        
        string userName; // имя пользователя в чате
        public MainWindow()
        {
            InitializeComponent();

            connection_txtBlock.Text = "Локальный порт: " + PORT + "\nХост: " + HOST + "\nВремя жизни пакета данных(TTL): " + TTL ;
            
            _context = SynchronizationContext.Current;

            btn_login.IsEnabled = true; // кнопка входа
            btn_logout.IsEnabled = false; // кнопка выхода
            btn_send.IsEnabled = false; // кнопка отправки
            
            groupAddress = IPAddress.Parse(HOST); //групповой адрес
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            userName = username_txtbox.Text;
            username_txtbox.IsReadOnly = true;

            try
            {
                client = new UdpClient(PORT); // указываем UDP порт
                
                client.JoinMulticastGroup(groupAddress, TTL); // присоединяемся к групповой рассылке
                
                Task receiveTask = new Task(ReceiveMessages); // запускаем задачу на прием сообщений
                receiveTask.Start();

                // отправляем первое сообщение о входе нового пользователя
                string message = "'" + userName + "'" + " вошел в чат";
                byte[] data = Encoding.UTF8.GetBytes(message); //переводим в поток байтов
                client.Send(data, data.Length, HOST, PORT); //* отправляем поток байтов на указанный хост и порт
                
                //Вкл/Выкл кнопок
                btn_login.IsEnabled = false;
                btn_logout.IsEnabled = true;
                btn_send.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReceiveMessages()
        {
            alive = true;
            try
            {
                while (alive)
                {
                    IPEndPoint remoteIp = null; // сетевая конечная точка в виде IP - адреса и номера порта.
                    byte[] data = client.Receive(ref remoteIp); // получаем ранний поток байтов 
                    string message = Encoding.UTF8.GetString(data); //сохраняем поток байтов в виде строки

                    // добавляем полученное сообщение в текстовое поле
                    _context.Post(delegate (object state) {
                        string time = DateTime.Now.ToShortTimeString();
                        chatBox_txtBlock.AppendText(time + " " + message + "\r\n");
                    }, null);
                }
            }
            catch (ObjectDisposedException)//Исключение, которое выбрасывается при выполнении операции над удаленным объектом.
            {
                if (!alive)
                    return;
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = String.Format("{0}: {1}", userName, msg_txtbox.Text); // Получаем формат сообщения

                byte[] data = Encoding.UTF8.GetBytes(message);// Получаем информацию в виде потока байтов

                if (msg_txtbox.Text != "Введите текст!" && !string.IsNullOrWhiteSpace(msg_txtbox.Text))
                    client.Send(data, data.Length, HOST, PORT);//* Отправка данных по хосту
                
                msg_txtbox.Clear();// Очистка поле текстбокса
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            ExitChat();
        }

        private void ExitChat()
        {
            string message = "'" + userName + "'" + " покидает чат";
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, HOST, PORT); //* 
            client.DropMulticastGroup(groupAddress); // клиент покидает группу

            alive = false;
            client.Close();

            username_txtbox.IsReadOnly = false;
            btn_login.IsEnabled = true;
            btn_logout.IsEnabled = false;
            btn_send.IsEnabled = false;
        }

        // placeholder для textbox
        private void msg_txtbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (msg_txtbox.Text == "Введите текст!")
                msg_txtbox.Text = "";
        }

        private void msg_txtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(msg_txtbox.Text))
                msg_txtbox.Text = "Введите текст!";
        }
    }
}
