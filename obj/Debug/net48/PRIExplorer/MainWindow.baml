   M S B A M L   `   `   `  ����F  BPRIExplorer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PRIExplorerPRIExplorer  5   '      PRIExplorer.MainWindow   6	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      6*   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6l   @d2http://schemas.microsoft.com/expression/blend/2008     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   +localclr-namespace:PRIExplorer     6'  -   5          � Title$  XamlingTool Prototype��5   O  $
��1000��6m  $	��600��6z   J� 	AllowDrop$
 True��6�   � 	Resources 5       !  CandidatesListViewContextMenu m� Command  CandidatesListViewStyle �� 
TargetType �� Property �� Value �� Width �� DisplayMemberBinding # ResourceMapTreeViewItemTemplate	 �� ItemsSource
 �� Orientation�� 5   
   %5  &	        &	 �     7   &	 >    �� 5      .@�5      m� $��Go to location��6    63   �� *GoToLocationCommand+m� 5      $��
Save as...��6    6/   �� *SaveAsCommand+5      �� 5	      " ��64   .�5
      ��  �@��View 5      �� 5      .P�5      �� $��
Qualifiers��6-   $	 300��6A    6M   �� *QualifiersDescription+�� 5      $��Location��6-   $	 300��6?    6K   �� *
Location+5      5      �� 5       � �� 5      �� 5      " ��6    .�5      ��  �@�ContextMenu8   6M   5      5      5      �� 5      	 6O   �� *
Children+.R�5      �� $
 
Horizontal=�6   $��0,1,0,1q�66   .+�5      �� $��0,0,4,0q�6   ��6-   �� *Icon+�� 5      ��6    �� *Name+5      5      5       .��5"      �  � RowDefinitions 5#   
   �� 5$      $
��Auto �6   �� 5%      $��1* �6   �� 5&      $
��Auto �6   �� 5'      $��2* �6    � ColumnDefinitions 5)   
   �� 5*      $��1* �6   �� 5+      $
��Auto �6   �� 5,      $��2* �6   .+�5.   
   o� $��0��6   $��3��6   $��0��63   .@�5/      m� $��_File��6   .@�50      m� $��_Open...��6    6-   �� *OpenCommand+�� 51      m� 52      $��_Close��6    6+   �� *CloseCommand+m� 53      $��_Save the pri as��6    65   �� *SaveAsPriCommand+54      m� 55      $��_Options��6   .@�56      m� $ ��Set resource _root path...��6    6?   �� *SetResourceRootPathCommand+57      58      R� 59   
   -   5        $��resourceMapTreeView��59      $��0��6.   $��1��6>   $��3��6K   #��[ 6\   ��6�   �� *	Entries+6�   �� 5:   
   $��1��6   $��1��6'   $��3��64   $��Center=�6E   $��Stretch=�6b   $��3��6~   �� 5;   
   -   5        $��candidatesListView��5;      $��2��6-   $��1��6=    �� SelectionMode$ Single=�6J   #��[ 6a   ��6�   �� *
Candidates+ �� SelectedItem 6�   �� *SelectedCandidate+ �� Mode$ TwoWay=��� 5<   
   $��2��6   $��2��6'   $��Stretch=�64   $��Stretch=�6R   $��3��6n    � 5=   
   -   5        $��previewFrame��5=      $��2��6$   $��3��64     � NavigationUIVisibility$ Hidden=�6A   ��6a   �� *PreviewContent+$ TwoWay=� � 5>   
   -   5        $��scopeDetailFrame��5>      $��1��6(   $��1��68   $��3��6E   $ Hidden=�6V   $��2��6v   $��3,0,0,40q�6�   �� 5?   
   $��2��6   $D��>Replace XBF (Just replaces, you don't need to save afterwards)��6!   $
��Left=�6j   $��
10,340,0,0q�6�   $��3��6�   $	��Top=�6�   $��25��6�   $	��346��6�   ��6�   �� *SaveXbfCommand+5@      5A      