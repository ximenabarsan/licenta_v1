   M S B A M L   `   `   `  ����H  DMedicalClinic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MedicalClinic.ViewsMedicalClinic.Views  5   	   *    $MedicalClinic.Views.AppoimentsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:MedicalClinic.Views     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  AppoimentsWindow��5
   	   $	��500��6"   $	��850��6/   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      6   .+�5   
   � $��0��6    5      �� 5      $
��2.5* �6    5      #  MaterialDesignThemes.Wpf.Card  5!   
   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�5"         ShadowEdges$ Bottom,Right=�5#      $��32q�5$      ���	#FF00B6FF$��80��5&      $	��180��5'      $	��Top=�5(      
�������$��Oblique$�5*      $
��Bold"�5+      $��46,14,38.8,0q�5,      $��0��5-      .��6   1.Alege specializare5/      �� 50   
   -   5         ��Name$ comboSpecialization��50      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Specializari��51      $��0��52      $��0��53      $��Center=�54      $	��180��55       &  "MaterialDesignFloatingHintComboBox#��[  57      
���   �$
��Bold"�6d   $��58��6v   $��46,110,38.8,42.4q�59   R   ��58      �� *Specializations+ �� SelectedItem 59      �� *SelectedSpecialization+ �� Mode$ TwoWay=�6R     5<   
   $ Depth3=�5=      $ Bottom,Right=�5>      $��32q�5?      ���	#FF00B6FF$��80��5A      $	��180��5B      $	��Top=�5C      
�������$��Oblique$�5E      $
��Bold"�5F      $��46,14,38.8,0q�5G      $��1��5H      $��0��5I      .��6   2.Alege Serviciul5K      �� 5L   
   -   5        $ comboServices��5L      $ Servicii��5M      $	��180��5N      $��1��5O      $��0��5P      $��46.2,112,38.6,44.4q�5Q      $��Center=�5R      #��[  5S      
���   �$
��Bold"�6d   $��54��6v   ��5T      �� *
Services+$ TwoWay=� 5U      �� *SelectedService+$ TwoWay=�  5V   
   $ Depth3=�5W      $ Bottom,Right=�5X      $��32q�5Y      ���	#FF00B6FF$��80��5[      $	��180��5\      $	��Top=�5]      
�������$��Oblique$�5_      $
��Bold"�5`      $��46,14,38.8,0q�5a      $��2��5b      $��0��5c      .��6   3.Alege Doctorul5e      �� 5f   
   -   5        $ comboDoctors��5f      $ Doctori��5g      $��46.4,108,38.4,48.4q�5h      $��2��5i      $��0��5j      $	��180��5k      #��[  5l      
���   �$
��Bold"�6\   $��54��6n   	 �� DisplayMemberPath$	 Value��5o      ��5m      �� *AvailableDoctors+$ TwoWay=� 5n      �� *SelectedDoctor+$ TwoWay=�5o        5p   
   $ Depth3=�5q      $ Bottom,Right=�5r      $��32q�5s      ���	#FF00B6FF$��80��5u      $	��180��5v      $	��Top=�5w      
�������$��Oblique$�5y      $
��Bold"�5z      $��46,14,38.8,0q�5{      $��0��5|      $��1��5}      .��6   4.Alege Data5      (  "System.Windows.Controls.DatePicker  5�   
   -   5        
  Name$
 FutureDatePicker��5�   	   $
 Data��5�   	   
���   �$
��Bold"�6B   $��56��6T   $��46,102.6,38.8,51q�5�   	   $��0��5�   	   $��1��6   $	��180��5�   	      DisplayDateStart 5�   	   �� *TomorrowDate+   DisplayDateEnd 5�   	   �� *MaxDateAppoiment+   SelectedDate 5�   	   �� *SelectedDate+5�        5�   
   $ Depth3=�5�      $ Bottom,Right=�5�      $��32q�5�      ���	#FF00B6FF$��80��5�      $	��180��5�      $	��Top=�5�      
�������$��Oblique$�5�      $
��Bold"�5�      $��46,14,38.8,0q�5�      $��1��5�      $��1��5�      .��6   5.Alege Ora5�      �� 5�   
   -   5        $ 
comboHours��5�      $	 Ora��5�      $��44.2,102.6,40.6,53q�5�      $	��180��5�      $��1��5�      $��1��5�      #��[  5�      
���   �$
��Bold"�6d   $��54��6v   ��5�      �� *AvailableHours+ 5�      �� *SelectedStartHour+5�   v   �� 5�   
    " MaterialDesignRaisedDarkButton#��[ 5�       � ToolTip$, &Apasa aici pentru a adauga programarea��5�      $��2��5�      $��1��5�      $��50��5�      $	��100��5�      ���	#FF00B6FF���	#FF00B6FF$	��Top=�5�      $��82.4,27.6,82.4,0q�6   $ Depth3=�5�      ��5�      �� *AddAppoimentCommand+.��5�   6   Adauga5�      �� 5�   
   #��[ 5�      $5 /Apasa aici pentru a reveni la pagina anterioara��5�      $��2��5�      $��1��5�      $��50��5�      $	��100��5�      ���	#FF00B6FF���	#FF00B6FF$	��Top=�5�      $��85.4,115.6,79.4,0q�6   $ Depth3=�5�      ��5�      �� *ShowPatientViewCommand+.��5�   6   Back5�      5�      5�      