   M S B A M L   `   `   `  ����H  DMedicalClinic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MedicalClinic.ViewsMedicalClinic.Views  5   	   &     MedicalClinic.Views.DoctorWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:MedicalClinic.Views     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  DoctorWindow��5	   	   $	��500��6    $	��850��6-   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $
��1.2* �6    � RowDefinitions 5   
   �� 5      $��1* �6   6*   .+�5   
   � $��0��6    5      �� 5      $
��0.6* �6    �� 5      $
��0.6* �6    �� 5      $
��0.6* �6    �� 5      $
��0.6* �6    �� 5      $
��0.6* �6    �� 5      $
��0.6* �6    �� 5      $
��0.4* �6    .+�5      � $��0��6   $��0��6#   .+�5       #  MaterialDesignThemes.Wpf.Chip  $��1��5!   (   
�������$
��Bold"�5#      $
��Auto��5$      $	��212��6   ���	#FF00B6FF   IconBackground �	#FF00B6FF$��
9,9,0,34.6q�5%   (   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�5&      ��5!   	   �� *
NameDoctor+   Icon 5'      '  !MaterialDesignThemes.Wpf.PackIcon  5)         Kind$ AccountOutline=�62   ���	#FF00B6FF$��36��6_   $��32��6k   5+      5-      � 5.      $	��200��6   $��0��6    $��1��60   $��Center=�6=   $	��Top=�6Z   .+�5/      ��    MaterialDesignRaisedButton#��[  50      $��60��51      $	��170��52      ���	#FF00B6FF���	#FF00B6FF$
��Bold"�54      +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 10��55       � ToolTip$) #Apasa aici pentru a-ti edita datele��56      $ Depth3=�57      $��10q�58      ��59      �� *ShowEditViewCommand+.��5:      �� $��Editeaza-ti datele��5;      5<      5>      � 5?      $��0��6   $��2��6#   .+�5@      �� #��[  5A      $��60��5B      $	��170��5C      ���	#FF00B6FF���	#FF00B6FF$
��Bold"�5E      $ 10��5F      $5 /Apasa aici pentru a-ti edita programul de munca��5G      $ Depth3=�5H      $��10q�5I      $	��Top=�5J      ��5K      �� *ShowScheduleCommand+.��5L      �� $��Editare program��5M      5N      5O      � 5P      $��0��6   $��3��6#   .+�5Q      �� #��[  5R      $��60��5S      $	��170��5T      ���	#FF00B6FF���	#FF00B6FF$
��Bold"�5V      $ 10��5W      $- 'Apasa aici pentru a adauga un tratament��5X      $ Depth3=�5Y      $��10q�5Z      $	��Top=�5\      ��5[      �� *ShowTreatmentCommand+.��5]      �� $��Adauga tratament��5^      5_      5`      � 5a      $��0��6   $��4��6#   .+�5b      �� #��[  5c      $��60��5d      $	��170��5e      ���	#FF00B6FF���	#FF00B6FF$
��Bold"�5g      $ 10��5h      $3 -Apasa aici pentru a-ti vizualiza programarile��5i      $ Depth3=�5j      $��10q�5k      $	��Top=�5m      ��5l      �� *ShowAppoimentsCommand+.��5n      �� $��
Programari��5o      5p      5q      � 5r      $��0��6   $��5��6#   .+�5s      �� #��[  5t      $��60��5u      $	��170��5v      ���	#FF00B6FF���	#FF00B6FF$
��Bold"�5x      $ 10��5y      $E ?Apasa aici pentru a vizualiza istorucul medical al unui pacient��5z      $ Depth3=�5{      $��10q�5|      $	��Top=�5~      ��5}      �� *ShowMedicalHistoryCommand+.��5      �� $��Pacienti��5�      5�      5�      �� 5�      $��0��6   $��6��6%   $��Right=�62   ���	#FF00B6FF���	#FF00B6FF$��Logout��5�      $��43��6&   $��80��63   $	��Top=�5�      $ Depth3=�5�      $��10q�5�      $��0,1.2,22.8,0q�6"   ��5�      �� *LogoutCommand+	 �� Mode$	 OneTime=�5�   "   5�      �� 5�   
   $��1��6   ���	#FF00B6FF���	#FF00B6FF��5�      �� 5�      
 �� ImageSource$1
 +/MedicalClinic;component/images/medical.jpg��6   5�      5�      5�      