   M S B A M L   `   `   `  ����L  HSortVisualization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:SortVisualizationSortVisualization  5   	   "    SortVisualization.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:SortVisualization     5   	     � Title$  
MainWindow��5   	    � WindowState$ 	Maximized=�5	   	   $	��350��5
   	   $	��680��5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��12* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $	��10* �6   �� 5      $��4* �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��1��5      
���iii�$��3q�5      
���   � �� CornerRadius$ 3��5      ._�5      �� -   5         ��Name$ canvas��5      5       5!      �� 5#   
   $��1��6   $��2��5$      $��2q�5%      
���   �$ 3��5'      ._�5(      �  5)      �� 5*      $
��Auto �6$   �� 5+      $��* �6$   .+�5.      �� $;��5Лог сравнений и перестановок��6   $��22(�5/      $��DemiBold"�50      
���OO/�$��0��52      �� 54      -   5        $��scrollViewer��55      $��1��54      .��56      �� -   5         ��Name$ logBox��56      ��57      �� 58      .R�59   "   ]� .+�5:   &   �� 
��������6,   ��  �� Path$ 
FirstPhase�6P   �� 5;   &   
��� �|���6,   �� $ SecondPhase�6Q   �� 5<   &   
���   ���6,   �� $ 
ThirdPhase�6P   5=   #   5>      5@      5A      5C      5D      �� 5F   
   $��1��6   $��0��5G      ._�5I      ��  �� Orientation$ 
Horizontal=�6   .+�5J      �� $��Скорость��6   $��15(�5K      �� 5L      -   5        $��slider��5Q      	 � Minimum$	 10Q�5L      
 � Maximum$

 1010Q�5M       �� TickFrequency$	 200Q�5N       �� TickPlacement$ BottomRight=�5O      $	��350��5P      5Q      �� 5R      $��Алгоритм��6   $��15(�5S      �� 5T      -   5        $��method��5T      $
��Auto��5U      $��Center=�5V      .@�5W      �� $��
Shell sort��6   �� 5X      $��
Quick sort��6   5Y      �� 5Z      -   5        $
��case��5[      $
��Auto��5Z      $��Center=�5\      .@�5]      �� $��Random filling��6   �� 5^      $��
Worse case��6   5_      �� 5`      $��
Длина��6   $��15(�5a      �� 5b      -   5        $��lenght��5b      $��20��5d      $��Center=�5e      $��Center=�5f      �� 5g      -   5        $��Начать��5g      5h      5i      5j      5k      5l      