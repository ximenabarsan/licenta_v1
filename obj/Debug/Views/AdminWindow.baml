   M S B A M L   `   `   `  ����H  DMedicalClinic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MedicalClinic.ViewsMedicalClinic.Views  5   	   %    MedicalClinic.Views.AdminWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:MedicalClinic.Views     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  AdminWindow��5	   	   $	��500��6   $	��850��6+      MaterialDesignBody#���   5
      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $
��1.2* �6    � RowDefinitions 5   
   �� 5      $��1* �6   6*   .+�5   
   � $��0��6    5      �� 5      $
��0.6* �6    �� 5      $
��0.6* �6    �� 5       $
��0.6* �6    �� 5!      $
��0.6* �6    �� 5"      $
��0.6* �6    �� 5#      $
��0.4* �6    .+�5&      � $	��200��6   $��0��6    $��1��60   $��Center=�6=   $	��Top=�6Z   .+�5'      ��   MaterialDesignRaisedButton#��[ 5(      $��60��5)      $	��170��5*      ���	#FF00B6FF���	#FF00B6FF$
��Bold"�5,      +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 10��5-       � ToolTip$) #Apasa aici pentru a-ti edita datele��5.      +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�5/      $��10q�50      ��51      �� *ShowRegisterForEdit+.��52      �� $��Editeaza-ti datele��53      54      56      � 57      $��0��6   $��2��6#   .+�58      �� #��[ 59      $��60��5:      $	��170��5;      ���	#FF00B6FF���	#FF00B6FF$
��Bold"�5=      $ 10��5>      $. (Apasa aici pentru a adauga un utilizator��5?      $ Depth3=�5@      $��10q�5A      $	��Top=�5B      ��5C      �� *ShowRegisterViewCommand+.��5D      �� $��Adauga utilizator��5E      5F      5G      � 5H      $��0��6   $��3��6#   .+�5I      �� #��[ 5J      $��60��5K      $	��170��5L      ���	#FF00B6FF���	#FF00B6FF$
��Bold"�5N      $ 10��5O      $* $Apasa aici pentru a edita serviciile��5P      $ Depth3=�5Q      $��10q�5R      $	��Top=�5S      ��5T      �� *ShowServicesViewCommand+.��5U      �� $��Adauga servicii��5V      5W      5X      � 5Y      $��0��6   $��4��6#   .+�5Z      �� #��[ 5[      $��60��5\      $	��170��5]      ���	#FF00B6FF���	#FF00B6FF$
��Bold"�5_      $ 10��5`      $, &Apasa aici pentru a edita utilizatorii��5a      $ Depth3=�5b      $��10q�5c      $	��Top=�5d      ��5e      �� *ShowEditUsersViewComand+.��5f      �� $��Editeaza utilizatorii��5g      5h      5i      �� 5k      $��0��6   $��5��6%   $��Right=�62   ���	#FF00B6FF���	#FF00B6FF$��Logout��5m      $��37��6&   $��92��63   $	��Top=�5o      $ Depth3=�5p      $��10q�5q      $��
0,1.8,11,0q�6"    J� RenderTransformOrigin$ 0.475,0.167&�66   ��5n      �� *LogoutCommand+	 �� Mode$	 OneTime=�5q   6   #  MaterialDesignThemes.Wpf.Chip  5t      $��1��5u   '   
�������$
��Bold"�5w      $
��Auto��5x      $	��212��6   ���	#FF00B6FF
   IconBackground
 �	#FF00B6FF$��
9,9,0,34.6q�5y   (   $ Depth3=�5z      ��5u   	   �� *	NameAdmin+   Icon 5{      '  !MaterialDesignThemes.Wpf.PackIcon  5}         Kind$ UserStarOutline=�6.   ���	#FF00B6FF$��36��6\   $��32��6h   5      5�      �� 5�   
   $��1.2,-1,-0.4,0q�6   $��1��6'   ���	#FF00B6FF���	#FF00B6FF��5�      �� 5�       �� ImageSource$1 +/MedicalClinic;component/images/medical.jpg��6   5�      5�      5�      