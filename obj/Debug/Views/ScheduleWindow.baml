   M S B A M L   `   `   `  ����H  DMedicalClinic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MedicalClinic.ViewsMedicalClinic.Views  5   	   (    "MedicalClinic.Views.ScheduleWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:MedicalClinic.Views     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  ScheduleWindow��5	   	   $	��500��6    $	��850��6-   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      .+�5   
   � $��0��6   $��0��6   .+�5      #  MaterialDesignThemes.Wpf.Card  +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�5         ShadowEdges$ Bottom,Right=�5      $��32q�5      ���	#FF00B6FF$��80��5      $	��180��5      $	��Top=�5      
�������$��Oblique$�5      $
��Bold"�5      $��46,14,38.8,0q�5       $��0��5!      .��6   1.Alege ziua dorita5#      �� 5$      -   5         ��Name$ comboSpecialization��5$      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Zi��5%      $��0��5&      $��0��5'      $��Center=�5(      $	��180��5)       �� DisplayMemberPath$ Value��5*       &  "MaterialDesignFloatingHintComboBox#��[  5+      
���   �$
��Bold"�6d   $��58��6v   $��46,110,38.8,42.4q�5-   G   ��5,      �� *Days+ �� SelectedItem 5-      �� *SelectedDay+	 �� Mode$	 TwoWay=�6G   51      � 52   
   $��0��6   $��1��6   .+�53        $ Depth3=�54      $ Bottom,Right=�55      $��32q�56      ���	#FF00B6FF$��80��58      $	��180��59      $	��Top=�5:      
�������$��Oblique$�5<      $
��Bold"�5=      $��46,14,38.8,0q�5>      $��1��5?      $��0��5@      .��6   2.Alege ora inceput5B      �� 5C      -   5        $ comboServices��5C      $ Ora inceput��5D      $	��180��5E      $��1��5F      $��0��5G      $��46.2,112,38.6,44.4q�5H      $��Center=�5I      #��[  5J      
���   �$
��Bold"�6d   $��54��6v   ��5K      �� *
StartHours+$	 TwoWay=� 5L      �� *	StartHour+$	 TwoWay=�5M      � 5N   
   $��2��6   $��0��6   .+�5O        $ Depth3=�5P      $ Bottom,Right=�5Q      $��32q�5R      ���	#FF00B6FF$��80��5T      $	��180��5U      $	��Top=�5V      
�������$��Oblique$�5X      $
��Bold"�5Y      $��46,14,38.8,0q�5Z      $��2��5[      $��0��5\      .��6   3.Alege ora iesire5^      �� 5_      -   5        $ comboDoctors��5_      $ Finalizare program��5`      $��46.4,108,38.4,48.4q�5a      $��2��5b      $��0��5c      $	��180��5d      #��[  5e      
���   �$
��Bold"�6\   $��54��6n   ��5f      �� *FinishHours+$	 TwoWay=� 5g      �� *
FinishHour+$	 TwoWay=�5i      �� 5j   
    " MaterialDesignRaisedDarkButton#��[ 5k      
 � ToolTip$.
 (Apasa aici pentru a adauga programaramul��5l      $��1��5n      $��1��5o      $��50��5p      $	��100��5q      ���	#FF00B6FF���	#FF00B6FF$	��Top=�5t      $��82.4,27.6,82.4,0q�6   $ Depth3=�5u      ��5m      �� *EditScheduleCommand+.��5u   6   Adauga5w      �� 5x   
   #��[ 5y      $5
 /Apasa aici pentru a reveni la pagina anterioara��5z      $��2��5|      $��1��5}      $��50��5~      $	��100��5      ���	#FF00B6FF���	#FF00B6FF$	��Top=�5�      $��86.4,28,78.4,0q�6   $ Depth3=�5�      ��5{      �� *ShowDoctorViewCommand+.��5�   6   Back5�      5�      5�      