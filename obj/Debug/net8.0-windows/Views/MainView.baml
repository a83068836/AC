   M S B A M L   `   `   `  ����=  9AC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ACAC  5   	   -clr-namespace:AC.ViewModelsAC.ViewModels  5      #clr-namespace:AC.IconsAC.Icons  5      F BTextEditLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:TextEditLib;assembly=TextEditLibTextEditLib 5	   	   L HFilterTreeViewLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulldDclr-namespace:FilterTreeViewLib.Behaviors;assembly=FilterTreeViewLibFilterTreeViewLib.Behaviors 5
   	   fEclr-namespace:FilterTreeViewLib.Converters;assembly=FilterTreeViewLibFilterTreeViewLib.Converters 5   	   fEclr-namespace:FilterTreeViewLib.ViewModels;assembly=FilterTreeViewLibFilterTreeViewLib.ViewModels 5   	   \@clr-namespace:FilterTreeViewLib.Views;assembly=FilterTreeViewLibFilterTreeViewLib.Views 5   	       AC.Views.MainView   5      S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  R NAvalonDock, Version=4.72.1.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4` \AvalonDock.Themes.VS2013, Version=4.72.1.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R	 NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^
 ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      	 
 5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml    	 5   	   @d2http://schemas.microsoft.com/expression/blend/2008    	 5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006    	 5   	   "localclr-namespace:AC    	 5   	   2
viewModelsclr-namespace:AC.ViewModels    	 5      (iconsclr-namespace:AC.Icons    	 5      E
avalonEdit.clr-namespace:TextEditLib;assembly=TextEditLib    	 5	   	   VbehavDclr-namespace:FilterTreeViewLib.Behaviors;assembly=FilterTreeViewLib    	 5
   	   VconvEclr-namespace:FilterTreeViewLib.Converters;assembly=FilterTreeViewLib    	 5   	   TvmEclr-namespace:FilterTreeViewLib.ViewModels;assembly=FilterTreeViewLib    	 5   	   Rviews@clr-namespace:FilterTreeViewLib.Views;assembly=FilterTreeViewLib    	 5   	   -   5          � Title$  
VS2013Test��5   	   $	��140��6   $	��116��6+   #��Z��6;   #��Z��6~   $	��900��6�   $
��1600��6�    J� SnapsToDevicePixels$
 True��6�      WindowForeground#���   6�     HeaderBackground#���  6(   � WindowStyle$
 None=�6X   � 
ResizeMode$ CanResizeWithGrip=�6k  '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
   -   5        
chrome��   ResizeBorderThickness$ 8q�5   '      CornerRadius$ 0��6A      GlassFrameThickness$ 0q�6R      NonClientFrameEdges$
 None=�6j   ��5      �� 5   
   	 �� 
TargetType"	 �6   .�5      �� 
 �� Property
 � �� �� Value$ 1q�60   �� 5      
 � ��  WindowBorderBrush# �  6,    �� Triggers 5      P� 5       P� Property �@�IsActive P� Value$ False��6.   . �5      �� 
 � ��  WindowInactiveBorderBrush# �  64   5      5      .��5"      � ��5#   
   �� 5$      "	 �6    5%      �� 5&       �� Binding 6"   �� *WindowState+ �� RelativeSource ��  �� AncestorType
 Window �� Value 	Maximized6w   .a�5'      �� 
 � ��$ 6,6,6,8q�63   5(      5*       � RowDefinitions 5-   
   �� 5.      $
��Auto �6   �� 5/      �� 50      $��23 �6   .+�53   
   � -   5         �Name$ header��53       54      �� 55      $
��Auto �6    �� 56      $��4 �6    �� 57      $
��Auto �6    .+�5:      �� $	��8,4q�6   $
��Left=�6&    �� Orientation$ 
Horizontal=�6A   .+�5;         AC.Icons.VisualStudioIcon  $	��4,0q�6)   $��20��66   ��5<      �� 5=      "	 >�6    .�5>      �� 
 � ��  	IconBrush# �  6;    5@      �� 5A   "    6.   �� *
IsActive+ �� *FindAncestor+" �	 False6�   .a�5B   &   �� 
 � ��  IconInactiveBrush# �  6C   5C   #   5E      5G      o� 5I        	MetroMenu#���  6   $��Center=�6;      IsHitTestVisibleInChrome$
 True��6V   .@�5J      m� $
��File��6   .@�5K      m� $	��New��6B    m� Command 6#   �� *
NewCommand+6B   m� 5L      $
��Open��6C    6#   �� *OpenCommand+6C   �� 5M      5R      m� 5T      $
��View��6   .@�5U      m� $��SolutionExplorer��6#    m� IsCheckable$
 True��6=    m� 	IsChecked 6P   �� *Explorer.IsVisible+ �� Mode$ TwoWay=�m� 5V      $��
Properties��6#   $
 True��67    6J   �� *Props.IsVisible+$ TwoWay=�m� 5W      $��
Error List��6#   $
 True��67    6J   �� *Errors.IsVisible+$ TwoWay=�m� 5X      $��Output��6#   $
 True��63    6F   �� *Output.IsVisible+$ TwoWay=�m� 5Y      $��Git Changes��6#   $
 True��68    6K   �� *Git.IsVisible+$ TwoWay=�m� 5Z      $��Toolbox��6#   $
 True��64    6G   �� *Toolbox.IsVisible+$ TwoWay=�5[      5\      �� 5^      $	��4,0q�5_      $
 True��5`       �� DisplayMemberPath$ Item1��5a      ��5b      �� *Themes+ �� SelectedItem 5c      �� *SelectedTheme+�� 5e        ProjectNameBackground#���  6    �� Padding$	 8,0q�6N   $
��14,0q�6\   ._�5f      �� $��VS2013 Demo��6    $��Center=�63    �� Padding$ 0,1,0,0q�6N   ��5g      �� 5h      "	 ��6$   .�5i   "   �� 
 � ��  ProjectNameForeground# �  6?    5j   "   �� 5k   &    62   �� *
IsActive+ �� *FindAncestor+" �	 False6�   .a�5l   *   �� 
 � �� !	 ProjectNameInactiveForeground# � 	 6G   5m   '   5o      5q      5r      5s      �� 5u      $ Vertical=�6   $��2��60   $
��Left=�6=   .+�5v      �� $
��12,1q�6   $��5��6*   $��17��64    � UseLayoutRounding$
 True��6@     �� BitmapScalingMode$  NearestNeighbor=�6Y   ��5w      L� 5x      ! j� TileMode$
! Tile=�6'   " j� Viewbox$" 0,0,4,4�67   # j� ViewboxUnits$# Absolute=�6I   $ j� Viewport$$ 0,0,4,4�6a   % j� ViewportUnits$% Absolute=�6t   & L� Drawing& 5y      � 5z   "    
 ToolBarGripper' � Brush#' � 
 62   ( � Geometry( 5{   &   � 5|   *   ��5~   2   �� ) �� Rect$) 0,0,1,1�6D   �� 5   2   $) 2,2,1,1�6D   5�   +   5�   #   5�      5�      �� 5�      $��
12,8,12,11q�6   $��5��60   $��17��6:   $
 True��6F   $  NearestNeighbor=�6_   ��5�      L� 5�      $
! Tile=�6'   $" 0,0,4,4�67   $# Absolute=�6I   $$ 0,0,4,4�6a   $% Absolute=�6t   & 5�      � 5�   "   #' � 
 62   ( 5�   &   � 5�   *   ��5�   2   �� $) 0,0,1,1�6D   �� 5�   2   $) 2,2,1,1�6D   5�   +   5�   #   5�      5�      5�      �� 5�      $ 
Horizontal=�6   $��Right=�62   $
 True��6N   .+�5�      �� -   5          MinimizeButton#��[ 5�      6A   �� 5�      -   5          MaximizeButton#��[ 5�      6A   �� 5�      -   5          RestoreDownButton#��[ 5�      6D   �� 5�      -   5          CloseButton#��[ 5�      6>   5�      5�        AvalonDock.DockingManager  5�   
   -   5        *  Name$* dockManager��5�      $��1��5�      +   AutoHideDelay$+ 0��5�      ,   ActiveContent, 5�      �� *ActiveDocument+$ TwoWay=�  ActiveDocumentConverter- �� 	Converter#- [ .   AnchorablesSource. 5�      �� *Tools+/   DocumentsSource/ 5�      �� *Files+0   Theme0 5�      �� *SelectedTheme.Item2+!1   LayoutItemTemplateSelector1 5�        AC.PanesTemplateSelector  5�      2   FileViewTemplate2 5�      �� 5�      .R�5�      � .+�5�   "     TextEditLib.TextEdit  3  Name$3 
textEditor��5�      F BAvalonEditB, Version=2.4.0.0, Culture=neutral, PublicKeyToken=null  AvalonEditB.TextEditor4   ShowLineNumbers$
4 True��5�      $��14(�5�   %   $,��&Cascadia Code,Consolas,Menlo,Monospace)�5�   %   5   Document5 5�      �� *
Document+6   
IsModified6 5�      �� *	IsDirty+$ TwoWay=�7   
IsReadOnly7 5�      �� *
IsReadOnly+$ OneWay=�8   SyntaxHighlighting8 5�      �� *HighlightingDefinition+$ OneWay=�9   Text.9 5�      5�   '   5�      5�      :   PropertiesViewTemplate: 5�      �� 5�      .R�5�      �� 5�      ;   ExplorerViewTemplate; 5�      �� 5�      .R�5�      � < � 	Resources< 5�   "   ?  9FilterTreeViewLib.Converters.LocationTypeToImageConverter   LocationTypeToImageConverter<  6FilterTreeViewLib.Converters.BoolToVisibilityConverter  boolToVisibilityConverter # boolToHiddenVisibilityConverter=   False>   True:	  4FilterTreeViewLib.Converters.InverseBooleanConverter  InverseBooleanConverter��%�   &	       &	      &	 &     &	 ]       5�   &   6H     5�   &   6E     5�   &   $= Hidden=�5�   )   $> Visible=�68   	  5�   &   6D   5�   #    5�   "   �� 5�   &   $
��Auto �64   �� 5�   &   $
��Auto �64   �� 5�   &   $��* �64   �� 5�   &   $
��Auto �64   .+�5�   "   � $��0��6'    5�   &   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68   .+�5�   &   � $��0��6+   $��2��68   ? � ColumnDefinitions? 5�   *   �� 5�   .   $��* �6?   �� 5�   .   $
��Auto �6?   .+�5�   *   #
  HandyControl.Controls.TextBox
  @ 
 Name$@ FilterTextBox��5�      $��0��5�   5   $��3q�6E   $��2��6P     TextBox.Small#��[ 6d   A � ToolTip$!A 请输入要搜索的内容��5�      ��5�      �� *SearchString+4  .FilterTreeViewLib.Behaviors.TextChangedCommandB   ChangedCommandB 5�      �� *SearchCommand+{�5�      �� *	IsLoading+$ OneWay=�#- [ �� 5�   *   $��Search��61   $��1��5�      $y�Hidden=�6%   $��3q�5�      ��5�      �� *SearchCommand+��5�      �� C �� ElementName$C FilterTextBox��D �� Path$
D Text�{�5�      �� *IsProcessing+$ OneWay=�#- [ 5�      5�   '   � 5�   &   $��1��6+   .+�5�   *   �� $��String is contained��63   $y�Hidden=�6Q   E d� 	IsChecked$
E TrueY�5�      $��3q�5�      {�5�      �� *IsProcessing+$ OneWay=�#- [ 5�      5�   '   5�   #   � 5  "   $��6��6.   $��1��69   F � IsIndeterminate$
F True��6F     ProgressBarBaseStyle#��[ 6]   y�5     �� *IsProcessing+$ OneWay=�#- [ R� 5  "   $��2��6+   $��3,0,3,3q�5     $��Stretch=�5       MetroTreeView#��[ 5	     G %� IsVirtualizing$
G True��5
     H %� VirtualizationMode$H 	Recycling=�5     I � DataContextI 5     �� *Root+��5     �� *CountryRootItems+$ OneWay=�2  ,FilterTreeViewLib.Behaviors.TreeViewBehaviorJ   DoubleClickCommandJ 5     �� *$"DataContext.ItemDoubleClickCommand+ �� " �K   EnterKeyCommandK 5     �� *!DataContext.ItemEnterKeyCommand+ �� " �< 5  &   *  $FilterTreeViewLib.Views.BindingProxy  DataContextProxyL   Data��%&   &	         5  *   L 6W   �� 5  '   ��5  &   �� 5  *   M �� DataType8  2FilterTreeViewLib.ViewModels.MetaLocationViewModel"M  6C   N �� ItemsSourceN 5  '   �� *
Children+$ OneWay=�.R�5  .   �� $ 
Horizontal=�69   .+�5  2   �� $��16��68   ��5  6   `� 5  :   O `� 	Converter#O [ 6G   .1�5  >   �� $D TypeOfLocation�6F   �� 5  >   $D 	LocalName�6F   �� 5  >   $D IsItemExpanded�6F   5  ;   5  3   �� 5!  2   $��5,0,0,0q�6d   ��6<   �� *	LocalName+$ OneTime=�<  6FilterTreeViewLib.Behaviors.HighlightTextBlockBehaviorP   RangeP 5"      �� *Range+A 5$  6   �� 5%  :   $
��Left=�6E   ��6`   �� *
FullPath+$ OneTime=�5(     5)  /   5*  +   ��5,  &   �� 5-  *   "	 Q�60     TreeViewItemBaseStyle.SmallQ �� BasedOn#Q [ 6S   .�5.  .   �� 6  0FilterTreeViewLib.Behaviors.TreeViewItemExpanded
 �@ Command 6c   �� $D Data.ExpandCommand�R �� Source#R [ $ OneTime=��� 5/  .   
 �@Q�
IsExpanded 6K   �� *IsItemExpanded+$ TwoWay=�50  +   52  #   � 54  "   $��3��6'   $��3q�64   .+�55  &   �� �� 56     ��6   �� *StatusStringResult+$ OneWay=� �� 57     ��6   �� *CountSearchMatches+$ OneWay=� 个匹配结果58     59  #   5:     5;     S   ErrorViewTemplateS 5=     �� 5>     .R�5?     �� 5@     T   OutputViewTemplateT 5B     �� 5C     .R�5D     �� 5E     U   GitChangesViewTemplateU 5G     �� 5H     .R�5I     �� 5J     V   ToolboxViewTemplateV 5L     �� 5M     .R�5N     �� 5O     5Q     'W    LayoutItemContainerStyleSelectorW 5T       AC.PanesStyleSelector  5U     X   	ToolStyleX 5V     �� 5W     .  (AvalonDock.Controls.LayoutAnchorableItem"	  6    .�5X     �� $  AvalonDock.Controls.LayoutItem
 �@ Title 66   �� *Model.Title+�� 5Y     
 �@ CanHide$
 True��68   �� 5Z     
 � y� 6;   �� *Model.IsVisible+$ TwoWay=�  BoolToVisibilityConverter#- [   System.Windows.VisibilityY   HiddenZ �� ConverterParameter#Z ZY �� 5[     
 �@ 	ContentId 6:   �� *Model.ContentId+�� 5\     
 �@ 
IsSelected 6;   �� *Model.IsSelected+$ TwoWay=��� 5]     
 �@ IsActive 69   �� *Model.IsActive+$ TwoWay=�5^     [   	FileStyle[ 5`     �� 5a     "	  6    .�5b     �� 
 �@ Title 66   �� *Model.Title+�� 5c     
 �@�ToolTip 68   �� *Model.FilePath+�� 5d     
 �@ CloseCommand 6=   �� *Model.CloseCommand+�� 5e     
 �@ 	ContentId 6:   �� *Model.ContentId+5f     5h     \   LayoutUpdateStrategy\ 5j       AC.LayoutInitializer  5k     ]   Layout.] 5n     "  AvalonDock.Layout.LayoutRoot  ^   	RootPanel.^ 5o     #  AvalonDock.Layout.LayoutPanel  _   Orientation$_ 
Horizontal=�6   �  �AvalonDock.Layout.LayoutGroup`1[[AvalonDock.Layout.ILayoutPanelElement, AvalonDock, Version=4.72.1.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]`   Children.` 5p       $_ Vertical=�6"   .` 5q     *  $AvalonDock.Layout.LayoutDocumentPane  1  +AvalonDock.Layout.LayoutAnchorablePaneGroup  5s     �  �AvalonDock.Layout.LayoutPositionableGroup`1[[AvalonDock.Layout.ILayoutAnchorablePane, AvalonDock, Version=4.72.1.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]a   
DockHeight$	a 128 �64   b   Orientation$b 
Horizontal=�6E   �  �AvalonDock.Layout.LayoutGroup`1[[AvalonDock.Layout.ILayoutAnchorablePane, AvalonDock, Version=4.72.1.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]c   Children.c 5t     ,  &AvalonDock.Layout.LayoutAnchorablePane  d   Name$d 
ErrorsPane��63     5u     $d 
OutputPane��63   5v     5w       5y     e   	DockWidth$	e 256 �60   $b Vertical=�6@   .c 5z       $d ExplorerPane��6/   �  �AvalonDock.Layout.LayoutPositionableGroup`1[[AvalonDock.Layout.LayoutAnchorable, AvalonDock, Version=4.72.1.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]f   
DockHeight$f 2* �6C     5{     $d PropertiesPane��6/   5|     5}     5~     5     �� 5�  
   $��2��6     StatusBarBackground#���  6   ._�5�     � .+�5�     �� $ 
Horizontal=�6   .+�5�     F� 
�������$��1��6*   $��Bottom=�6>   $��	7.5,0,8,5q�6Y   ��5�     �� 5�     $) 	0,0,12,10�60   z�5�     �� 5�     g �� AngleX$	g -15Q�6,   5�     5�     �� 5�     $��Ready��6      StatusBarForeground#���  6-   $��Center=�6`   $��0,0,6,1q�6{   5�     5�     5�     5�     5�     