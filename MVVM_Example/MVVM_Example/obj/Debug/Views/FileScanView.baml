   M S B A M L   `   `   `  ����  MVVM_Example%    MVVM_Example.Views.FileScanView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  FileScanView��5   	   $	��300��6   $	��480��6+   .��5      � .+�5   
   �� -   5        $��targetDirectory��5   �   $��23��6-   $
��Left=�69   $��	15,10,0,0q�6T   $	��Top=�6g   $	��280��6   {�.  ��6   �� *	Directory+6�   �� 5   
   -   5        $��btnStart��5   [   $
��Scan��6   $��23��6    $
��Left=�6,   $��
300,10,0,0q�6G   $	��Top=�6k   $��75��6�   ��6�   �� *	StartScan+�� 5   
   -   5        $��	btnCancel��5   ]   $��Cancel��6   $��23��6"   $
��Left=�6.   $��
380,10,0,0q�6I   $	��Top=�6n   $��75��6�   ��6�   �� *
CancelScan+�� 5	   
   -   5        $��	listFiles��5	      $��
-2,-15,0,0q�6#   $	��170��6^   $	��440��6k   ��67   �� *DuplicateFiles+5
      � 5   
   $��	0,225,0,0q�6   
����ح�.+�5      � -   5        $��progressBar��5   5   $��20��6   $��20��6&   $	��440��6�    � Value 6H   �� *CurrentProgress+ �� UpdateSourceTrigger$ PropertyChanged=�y�6�   �� *ProgressVisibility+$ PropertyChanged=��� 5      -   5        $��progressText��5      $��Center=�6o   $��Center=�6�   ��6,   �� *ProgressText+$ PropertyChanged=�6�   5      5      5      