   M S B A M L   `   `   `  ����H  DMedicalClinic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MedicalClinic.ViewsMedicalClinic.Views  5   	   .    (MedicalClinic.Views.EditUsersWindowAdmin   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:MedicalClinic.Views     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  EditUsersWindowAdmin��5	   	   $	��500��6'   $	��850��64   .��5
      �  � RowDefinitions 5   
   �� 5      $
��2.5* �6   5      �� 5      $��1* �6   .+�5   
   &   System.Windows.Controls.DataGrid  +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�5      $��32q�5      $��0��5      
�������   CanUserSortColumns .    CanUserAddRows .     AutoGenerateColumns .  -  'MaterialDesignThemes.Wpf.DataGridAssist   CellPadding$ 13 8 8 8q�5         ColumnHeaderPadding$ 8q�5      ��5      �� *Users+ �� UpdateSourceTrigger$ PropertyChanged=�	 �� SelectedItem	 5      �� *SelectedUser+
 �� Mode$
 TwoWay=�$ PropertyChanged=�   Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$
 Nume��5$       0  ,MaterialDesignDataGridTextColumnEditingStyle1  +System.Windows.Controls.DataGridBoundColumn   EditingElementStyle# [  5%         Binding 5#      �� *
nameUser+5%      1  +MaterialDesignThemes.Wpf.DataGridTextColumn  5&      $ Prenume��5(       5 1MaterialDesignDataGridTextColumnPopupEditingStyle# [ 5)       5'      �� *surnameUser+5)        5*      $ Email��5,      # [  5-       5+      �� *email+5-        5.      $ Parola��50      # [  51       5/      �� *
password+51      56      �� 58   
   -   5         ��Name$
 Name��59      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 Nume��5:      $��1��5;      $	��Top=�5<      $
��Left=�5=      $��25��5>      $	��100��5?      $��59,3,0,0q�5@   \    J� RenderTransformOrigin$ 0.5,0.5&�6n   ��6   �� *SelectedUser.nameUser+$
 TwoWay=�$ PropertyChanged=�6n   �� 5A   
   -   5        $ Surname��5B      $ Prenume��5C      $	��Top=�5D      $
��Left=�5E      $��25��5F      $	��100��5G      $��	200,3,0,0q�5H   _   $��1��6r   ��6   �� *SelectedUser.surnameUser+$
 TwoWay=�$ PropertyChanged=�6r   �� 5I   
   -   5        $ email��5J      $ Email��5K      $	��Top=�5L      $
��Left=�5M      $��25��5N      $	��100��5O      $��	360,3,0,0q�5P   Y   $��1��6l   ��6   �� *SelectedUser.email+$
 TwoWay=�$ PropertyChanged=�6l   �� 5Q   
   -   5        $ password��5R      $ Parola��5S      $	��Top=�5T      $
��Left=�5U      $��25��5V      $	��100��5W      $��1��5X      $��	509,0,0,0q�5Y   \   ��6   �� *SelectedUser.password+$
 TwoWay=�$ PropertyChanged=�6\   �� 5[   
     MaterialDesignIconButton#��[ 5\   
    � ToolTip$ Apasa aici pentru a edita��5]   
   ���	#FF00B6FF$��50��5^   !   $��50��6-   $��176,50,560.8,24.4q�68   $��1��6S   ��5[      �� *EditUserCommand+.��5_      '	  !MaterialDesignThemes.Wpf.PackIcon	   	  Kind$ UserEdit=�5`      $��30��6   $��33��6)   
�������64   5c      �� 5e   
   #��[ 5f   
   $  Apasa aici pentru a setrge��5g   
   ���	#FF00B6FF$��50��5h   !   $��50��6-   $��1��68   $��438,46,298.8,28.4q�6F   ��5e      �� *DeleteUserCommand+.��5i      	  $ DeleteAlert=�5j      $��30��6    $��30��6,   
�������67   5k      �� 5n   
   $��1��6   $
��Back��5p      ���	#FF00B6FF���	#FF00B6FF$��35��5s      $	��100��5t      $ Depth3=�5u      $
��Bold"�5w      $��20(�5x      $��657,52,39.8,32.4q�5y      ��5v      �� *ShowAdminViewCommand+5y      5{      5|      