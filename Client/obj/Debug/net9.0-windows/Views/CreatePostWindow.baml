   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Client.ViewsClient.Views  5      #    Client.Views.CreatePostWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Client.Views     5      LmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes     5        � Title$  CreatePostPage��5	      $	��800��6   ���#F8F8F8.��5      � $��40q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Новий пост��6   $��26(�5      $��SemiBold"�5      $��0 0 0 20q�5      ���#2225      �� 5   
   $��1��6   .+�5      �� $��Тема��6   $��16(�6$   64   �� 5      
������� �� CornerRadius$ 16��6(    �� Padding$ 16q�6:   $��0,0,0,10q�6G   ���#DDD$��1q�6l   ._�5      �� -   5         ��Name$ TitleBox��5      $��16(�5       
������ $��0q�5"      ���#1115#      5$      �� 5'      $/��)Напишіть щось цікаве...��6   $��15(�67   $��
0, 0, 0, 5q�6E   �� 5(      
�������$ 16��6(   $ 16q�6:   ���#DDD$��1q�6Z   $	��250��6n   ._�5)      �� -   5        $ BodyBox��5)      $��15(�5*      
������ $��0q�5,       �� AcceptsReturn$
 True��5-       �� TextWrapping$
 Wrap=�5.      " �� VerticalScrollBarVisibility$
 Auto=�5/      ���#11150      51      53      �� 56   
   -   5        $��2��56      $��Опублікувати��57      $��50��58      $��0 30 0 0q�59      ���#007AFF
�������$��SemiBold"�5<      $��16(�5=      $��0q�5>       � Cursor$
 Hand��5?      5@      5A      5B      