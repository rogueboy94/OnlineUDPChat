   M S B A M L   `   `   `  ����E  AOnlineChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:OnlineChat
OnlineChat  5   	       OnlineChat.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:OnlineChat     5   	     � Title$  Чат��5   	   $
��Auto��6   $	��525��6#    � 
ResizeMode$ NoResize=�6/   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��150px��6   60   �� 5      $��100px��6   60   .+�5   
   9� $'��!Сетевые Настройки��6   .��5      �� -   5         ��Name$ connection_txtBlock��5      67   5      �� 5   
   $
��16px(�6   $��
Логин��6#   $��1��62   .��5      �  � ColumnDefinitions 5      �� 5      6)   �� 5      6)   �� 5      6)    5      �� 5      6&   �� 5      6&   .+�5      �� $��0��6    �� TextAlignment$ Center=�6,   $��Center=�6C   $��Center=�6`   Введите имя6�   �� 5      -   5         ��Name$ username_txtbox��5      $��1��63   6E   �� 5       -   5         ��Name$ 	btn_login��5       $��2��6,   .��6T   
Вход6Z   �� 5!      -   5        $ 
btn_logout��5!      $��2��6-   $��1��6=   .��6c   
Выход6j   5"      5#      �� 5$   
   -   5        $ chatBox_txtBlock��5$      
�������$��2��6?   
������� �� TextWrapping$
 Wrap=�6^   $
��18px(�6r   "	 �� VerticalScrollBarVisibility$
	 Auto=�6�   6�   �� 5%   
   $��3��6   .��5&      �  5'      �� 5(      $��2* �6'   64   �� 5)      6)   .+�5+      �� -   5        $ 
msg_txtbox��5+      $
��16px(�6.   $
	 Auto=�6~   $
 Wrap=�6�   .��6�   Введите текст!6�   �� 5,      -   5        $ btn_send��5,      $��1��6+   $
��18px(�6;   $��Black"�6K   
�������.��6�   Отправить6�   5-      5.      5/      50      