   M S B A M L   `   `   `  ����H  DJohnWoodStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:JohnWoodStore.ViewJohnWoodStore.View  5   	   &     JohnWoodStore.View.CatalogWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:JohnWoodStore.View     5   	     � Title$  CatalogWindow��5   	   $	��550��6   $
��1000��6,    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ CanMinimize=�5
   	    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $$ Styles/CatalogWindowStyle.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $��10 �6   �� 5      $	��120 �6   �� 5      �� 5      $��10 �6   .+�5   
   ��    LogoText#��[  6   $��1��5      �� 5   
     	UserPanel#��[ 6   $��1��5       .+�5!      ��   UserInfoButtons#��[ 6   $��профиль��5#      ��5"      �� *ShowUserInfo+5#      �� 5$      #��[ 6   $��корзина��5&      ��5%      �� *
OpenCart+5&      5'      �� 5)   
     CatalogFunctionsPanel#��[ 6   $��1��5*      .+�5+      �� -   5          InfoBox#��[ 5+       �� SelectedItem 5,      �� *SelectedCategoryName+��5-      �� *CategoriesName+5.      �� 5/      -   5        #��[ 5/      $	��200��50       51      �� *SelectedFilterName+��52      �� *	Filters+53      �� 54      -   5         ��Name$ SearchField��55        	SearchBox#��[ 54      .��56   8   	 J� InputBindings	 57      �� 58      
 �� Gesture$
 Enter��6!    �� Command 61   �� *Search+ �� CommandParameter 6L   ��  �� ElementName$ SearchField�� �� Path$
 Text�.��59   )   5:      5;      �� 5=   
   $��2��6    �� Orientation$ 
Horizontal=�6"   .+�5>      ��   ProductsPanel#��[ 6   .+�5?      &   System.Windows.Controls.DataGrid  -   5          Name$ ProductGrid��5@        CatalogGrid#��[ 5?         AutoGenerateColumns$ False��5C         
IsReadOnly$
 True��5D         CanUserSortColumns$ False��5E      $��2��5F      ��5B      �� *
Products+ �� NotifyOnSourceUpdated$
 True�� �� UpdateSourceTrigger$ PropertyChanged=�   Columns 5G      0  *System.Windows.Controls.DataGridTextColumn  5H      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5I   -   �� *Name+  5J      $ Цена��6-    5K   -   �� *Price+  5L      $ Описание��6-    5M   -   �� *Description+  5N      $ Категория��6-    5O   -   �� *CategoryName+5Q      5R      �� 5S        ProductButtonsPanel#��[ 6   .+�5T         	 SelectedProductsGrid#��[	 6   $ False��5V      ��5U      �� *SelectedProducts+$
 True��$ PropertyChanged=� 5W        5X      $ Название��6-      
IsReadOnly$
 True��5Y   -    5Z   -   �� *Name+  5[      $ Категория��6-   $
 True��5\   -    5]   -   �� *CategoryName+  5^      $ Количество��6-    5_   -   �� *Count+5a      5b      5c      5e      5g      