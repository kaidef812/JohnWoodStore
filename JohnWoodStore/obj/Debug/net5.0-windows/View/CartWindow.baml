   M S B A M L   `   `   `  ����H  DJohnWoodStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:JohnWoodStore.ViewJohnWoodStore.View  5   	   #    JohnWoodStore.View.CartWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:JohnWoodStore.View     5   	     � Title$  
CartWindow��5   	   $	��450��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ CanMinimize=�5
   	    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $! Styles/CartWindowStyle.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $��10 �6   �� 5      $	��120 �6   �� 5      �� 5      $��10 �6   .+�5   
   ��    LogoText#��[  6   $��1��5      �� 5   
     	UserPanel#��[ 6   $��1��5       .+�5!      ��   UserInfoButtons#��[ 6   $��
назад��5#      ��5"      �� *GoBack+5#      �� 5$      #��[ 6   $��оформить��5&      ��5%      �� *Pay+5&      5'      �� 5)   
     TotalInfoPanel#��[ 6   $��1��5*      .+�5+      ��   	TotalText#��[ 6   $��К оплате: ��5,      �� 5-      #��[ 6   ��5.      �� *	TotalCost+5/      &   System.Windows.Controls.DataGrid  51   
     CatalogGrid#��[ 6      AutoGenerateColumns$ False��53         
IsReadOnly$
 True��54      $��2��55      ��52      �� *
Products+	 �� NotifyOnSourceUpdated$
	 True��
 �� UpdateSourceTrigger$
 PropertyChanged=�   Columns 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 58   %   �� *Name+  59      $ Категория��6%    5:   %   �� *CategoryName+  5;      $ Цена��6%    5<   %   �� *Price+  5=      $ Количество��6%    5>   %   �� *Count+5@      5B      5D      