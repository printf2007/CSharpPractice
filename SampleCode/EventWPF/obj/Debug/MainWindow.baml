   M S B A M L   `   `   `  ����C  ?EventWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:EventWPFEventWPF  5   	       EventWPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:EventWPF     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � Icon$ icon.jpg��65    � ShowInTaskbar .  .��5	      � -   5         � ColumnDefinitions 5
   
   �� 5      $
��136* �6   �� 5      $	��77* �6   �� 5      $
��343* �6   �� 5      $
��238* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��4��6;   $��0,0,-0.185,58.782q�6O   $��2��6j   .+�5      �� -   5         ��Name$ CheckStopPreview��5      $��20q�61   .��6=   Stop Preview6K   �� 5      -   5        $ CheckStopBubbling��5      $��20q�62   .��6>   Stop Bubbling6M   �� 5      -   5        $ CheckIgnoreGridMove��5      $��20q�64   .��6@   Ignore Grid Move6R   �� 5      -   5        $��20q�5      .��67   Clean Status6E   5      �� 5   
   -   5         ��Name$ button1��5      $��1��6"   $��20,20.151,19.815,19.782q�6/   $��4��6�   .��6�   Move6�   �� 5   
   $��2��6   $��4��6$   $��0,0.218,-0.185,0.109q�68   .��5      �� -   5         ��Name$ 
textStatus��5   $   $��20q�6   6;   5      5      5       