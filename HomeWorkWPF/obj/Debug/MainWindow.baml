   M S B A M L   `   `   `  ����F  BHomeWorkWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:HomeWorkWPFHomeWorkWPF  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       HomeWorkWPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:HomeWorkWPF     5   	   9system&clr-namespace:System;assembly=mscorlib     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��690��6)    � 	Resources 5
         HomeWorkWPF.Employee   IdConverter��%   &	          5   
   6   5      .��5      �  � RowDefinitions 5   
   �� 5      $	��250 �6   6+   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $	��450 �6   6-   �� 5      $��* �6   6+   .+�5   
   �� 
���   �$��1q�6%   $	��250��69   $	��450��6F   $��0��6R   $��0��6c   ._�5      �� .+�5      �� -   5         ��Name$ ListViewName��5      $	��220��6W   ��6d   ��  �� View 5      �� 5      .P�5      �� $��Работник��6-    �� DisplayMemberBinding 6?   �� *FIO+�� 5      $��Возраст��6-    6>   �� *	BirthDate+ �� StringFormat$ 
dd.MM.yyyy���� 5      $��Предприятие��6-    �� CellTemplate 5   "   �� 5   &   .R�5    *   �� 
����  �$
��Bold"�6b   ��64   �� *
Department+6v   5!   '   5#      5$      5&      �� 5'      	 �� Orientation$	 
Horizontal=�6   .+�5(      �� -   5        $	��138��5(      $��5q�6)   .��6V   Изменить6`   �� 5)      -   5        $	��138��5)      $��5q�6)   $��Добавить��6S   �� 5*      -   5        $	��138��5*      $��5q�6)   $��Удалить��6V   5+      5,      5-      �� 5.   
   $��1��6   $��0��6!   
���   �$��1q�6B   ._�5/      �� .+�50      �� -   5        
 ��Name$
 ComboBoxName��50       �� DisplayMemberPath$ 
Department��61   $
��Left=�6P   $��	5,5,5,160q�6k   $	��Top=�6~   $	��210��6�   �� 51      -   5        $��Изменить��51      $
��Left=�6,   $��5q�6G   $	��Top=�6R   $	��210��6j   {�.  6�   �� 52      -   5        $��Добавить��52      $
��Left=�6,   $��5q�6G   $	��Top=�6R   $	��210��6j   {�.  6�   53      54      �� 55   
   $��2��6   $��0��6%   $��1��65   
���   �$��1q�6V   ._�56      �� .+�57      �� $��0 130q�6   .@�58      �� .��59      �� -   5         ��Name$ StatusBarWorkerName��59       6?   5:      �� 5;      �� 5<      .��5=      �� -	   5        $ StatusBarWorkerId��5=       6=   5>      5?      5@      5A      5B      5C      