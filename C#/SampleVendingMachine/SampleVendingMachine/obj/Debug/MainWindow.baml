   M S B A M L   `   `   `  ����O  KSampleVendingMachine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SampleVendingMachineSampleVendingMachine  5   	   %    SampleVendingMachine.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:SampleVendingMachine     5   	   -   5          � Title$  
MainWindow��5   	   $	��850��6   $	��600��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ image��5
      $
��Left=�6   $	��202��6:   $��	30,10,0,0q�6G   $	��Top=�6Z   $��88��6r   $��drink_base_image2.png��6}   A� 5   
   -   5        ���	#FF00DCFF$
��Left=�5   #   $��37��6>   $��
30,217,0,0q�6J   
���   �$	��Top=�6m   $��88��6�   6�   �� 5   
   -   5         ��Name$ label��5      $
��\100��6   $
��Left=�6.   $��
47,215,0,0q�6I   $	��Top=�6]   $��39��6u   $��53��6�   $
��Bold"�6�   $��20(�6�   6�   �� 5   
   -   5        $ 
image_Copy��5      $
��Left=�6$   $	��202��6?   $��
177,10,0,0q�6L   $	��Top=�6`   $��88��6x   $��drink_base_image2.png��6�   A� 5   
   -   5        ���	#FF00DCFF$
��Left=�5   #   $��37��6>   $��177,217,0,0q�6J   
���   �$	��Top=�6n   $��88��6�   6�   �� 5   
   -   5        $ 
label_Copy��5      $
��\100��6$   $
��Left=�63   $��194,215,0,0q�6N   $	��Top=�6c   $��39��6{   $��53��6�   $
��Bold"�6�   $��20(�6�   6�   �� 5   
   -   5        $ image_Copy1��5      $
��Left=�6%   $	��202��6@   $��
328,10,0,0q�6M   $	��Top=�6a   $��88��6y   $��drink_base_image2.png��6�   A� 5   
   -	   5        ���	#FF00DCFF$
��Left=�5   #   $��37��6>   $��328,217,0,0q�6J   
���   �$	��Top=�6n   $��88��6�   6�   �� 5   
   -
   5        $ label_Copy1��5      $
��\100��6%   $
��Left=�64   $��345,215,0,0q�6O   $	��Top=�6d   $��39��6|   $��53��6�   $
��Bold"�6�   $��20(�6�   6�   �� 5   
   -   5        $ image_Copy2��5      $
��Left=�6%   $	��202��6@   $��
476,10,0,0q�6M   $	��Top=�6a   $��88��6y   $��drink_base_image2.png��6�   A� 5   
   -   5        ���	#FF00DCFF$
��Left=�5   #   $��37��6>   $��476,217,0,0q�6J   
���   �$	��Top=�6n   $��88��6�   6�   �� 5   
   -   5        $ label_Copy2��5      $
��\100��6%   $
��Left=�64   $��492,215,0,0q�6O   $	��Top=�6d   $��39��6|   $��54��6�   $
��Bold"�6�   $��20(�6�   6�   �� 5   
   -   5        $ image_Copy3��5      $
��Left=�6%   $	��202��6@   $��
30,281,0,0q�6M   $	��Top=�6a   $��88��6y   $��drink_base_image2.png��6�   A� 5   
   -   5        ���	#FF00DCFF$
��Left=�5   #   $��36��6>   $��
30,489,0,0q�6J   
���   �$	��Top=�6m   $��88��6�   6�   �� 5   
   -   5        $ label_Copy3��5      $
��\150��6%   $
��Left=�64   $��
47,487,0,0q�6O   $	��Top=�6c   $��38��6{   $��53��6�   $
��Bold"�6�   $��20(�6�   6�   �� 5   
   -   5        $ image_Copy4��5      $
��Left=�6%   $	��202��6@   $��177,281,0,0q�6M   $	��Top=�6b   $��88��6z   $��drink_base_image2.png��6�   A� 5   
   -   5        ���	#FF00DCFF$
��Left=�5   #   $��36��6>   $��177,489,0,0q�6J   
���   �$	��Top=�6n   $��88��6�   6�   �� 5   
   -   5        $ label_Copy4��5      $
��\150��6%   $
��Left=�64   $��194,487,0,0q�6O   $	��Top=�6d   $��38��6|   $��53��6�   $
��Bold"�6�   $��20(�6�   6�   �� 5   
   -   5        $ image_Copy5��5      $
��Left=�6%   $	��202��6@   $��328,281,0,0q�6M   $	��Top=�6b   $��88��6z   $��drink_base_image2.png��6�   A� 5   
   -   5        ���	#FF00DCFF$
��Left=�5   #   $��36��6>   $��328,489,0,0q�6J   
���   �$	��Top=�6n   $��88��6�   6�   �� 5   
   -   5        $ label_Copy5��5      $
��\150��6%   $
��Left=�64   $��345,487,0,0q�6O   $	��Top=�6d   $��38��6|   $��53��6�   $
��Bold"�6�   $��20(�6�   6�   �� 5   
   -   5        $ image_Copy6��5      $
��Left=�6%   $	��202��6@   $��476,281,0,0q�6M   $	��Top=�6b   $��88��6z   $��drink_base_image2.png��6�   A� 5    
   -   5        ���	#FF00DCFF$
��Left=�5    #   $��36��6>   $��476,489,0,0q�6J   
���   �$	��Top=�6n   $��88��6�   6�   �� 5!   
   -   5        $ label_Copy6��5!      $
��\150��6%   $
��Left=�64   $��492,487,0,0q�6O   $	��Top=�6d   $��38��6|   $��54��6�   $
��Bold"�6�   $��20(�6�   6�   � 5"   
   $
��Left=�6   $��75��6*   $��218,542,0,0q�66   $	��Top=�6K   $	��216��6c   
���   �6o   A� 5#   
   -   5        ���	#33000000$
��Left=�5#   #   $��75��6>   $��476,542,0,0q�6J   
���   �$	��Top=�6n   $��88��6�   6�   A� 5$   
   -   5        ���	#72000000$
��Left=�5$   #   $��21��6>   $��476,568,0,0q�6J   
���   �$	��Top=�6n   $��88��6�   6�   A� 5%   
   ���	#FFE6E61D$
��Left=�6#   $��22��6>   $��480,637,0,0q�6J   
���   �$	��Top=�6n   $��99��6�   A� 5&   
   ���	#FFE6E61D$
��Left=�6#   $��56��6>   $��473,637,0,0q�6J   
���   �$	��Top=�6n   $��61��6�   �� 5'   
   -   5        $ image1��5'      $
��Left=�6    $��162                ��6;   $��
28,637,0,0q�5(      $	��Top=�6&   $	��400��6>   $��off_drink_image.png��6J   �� 5)   
   -   5        $ image2��5)      $
��Left=�6    $��88��6;   $��476,711,0,0q�6G   $	��Top=�6\   $��88��6t   $��off_coin_image.png��6   �� 5*   
   -   5        $ image_coin1��5*      $
��Left=�6%   $��75��6@   $��391,542,0,0q�6L   $	��Top=�6a   $��40��6y   $��seg1_image.png��6�   �� 5+   
   -   5        $ image_coin2��5+      $
��Left=�6%   $��75��6@   $��349,542,0,0q�6L   $	��Top=�6a   $��40��6y   $��img/seg2_image.png��6�   �� 5,   
   -    5        $ image_coin3��5,      $
��Left=�6%   $��75��6@   $��307,542,0,0q�6L   $	��Top=�6a   $��40��6y   $��seg3_image.png��6�   �� 5-   
   -!   5        $ image_coin4��5-      $
��Left=�6%   $��75��6@   $��265,542,0,0q�6L   $	��Top=�6a   $��40��6y   $��seg4_image.png��6�   �� 5.   
   -"   5        $ image_coin5��5.      $
��Left=�6%   $��75��6@   $��223,542,0,0q�6L   $	��Top=�6a   $��40��6y   $��img/seg5_image.png��6�   5/      50      