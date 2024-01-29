using System.Collections.Generic;

namespace XbfAnalyzer.Xbf;

public static class XbfFrameworkTypes
{
	private static readonly string[] typeNames = new string[972]
	{
		"Byte", "Char16", "DateTime", "GeneratorPosition", "Guid", "Int16", "Int64", "Object", "Single", "TypeName",
		"UInt16", "UInt32", "UInt64", "AutomationProperties", "DataPackage", "DataTemplateSelector", "DependencyObject", "EventHandler", "GroupStyleCollection", "GroupStyleSelector",
		"ItemContainerGenerator", "ListViewPersistenceHelper", "StyleSelector", "TextOptions", "ToolTipService", "Typography", "Uri", "MediaCapture", "PlayToSource", "MediaProtectionManager",
		"Application", "ApplicationBarService", "AutomationPeer", "AutoSuggestBoxSuggestionChosenEventArgs", "AutoSuggestBoxTextChangedEventArgs", "Boolean", "Brush", "CacheMode", "CollectionView", "CollectionViewGroup",
		"CollectionViewSource", "Color", "ColorKeyFrame", "ColumnDefinition", "ComboBoxTemplateSettings", "CornerRadius", null, "DebugSettings", "DependencyObjectWrapper", "DependencyProperty",
		"Deployment", "Double", "DoubleKeyFrame", null, "EasingFunctionBase", "Enumerated", "ExternalObjectReference", "FlyoutBase", "FontFamily", "FontWeight",
		"FrameworkTemplate", "GeneralTransform", "Geometry", "GradientStop", "GridLength", "GroupStyle", "HWCompNode", "ImageSource", "IMECandidateItem", "IMECandidatePage",
		"InertiaExpansionBehavior", "InertiaRotationBehavior", "InertiaTranslationBehavior", "InputScope", "InputScopeName", "Int32", "IRawElementProviderSimple", "KeySpline", "LayoutTransitionStaggerItem", "LengthConverter",
		"ListViewBaseItemTemplateSettings", "ManipulationDelta", "ManipulationPivot", "ManipulationVelocities", "MarkupExtensionBase", "Matrix", "Matrix3D", "NavigationTransitionInfo", "ObjectKeyFrame", "PageStackEntry",
		"ParametricCurve", "ParametricCurveSegment", "PathFigure", "PathSegment", "Point", "Pointer", "PointerKeyFrame", "PointKeyFrame", "PresentationFrameworkCollection", "PrintDocument",
		"ProgressBarTemplateSettings", "ProgressRingTemplateSettings", "Projection", "PropertyPath", "Rect", "RowDefinition", "SecondaryContentRelationship", "SetterBase", "SettingsFlyoutTemplateSettings", "Size",
		"SolidColorBrushClone", "StaggerFunctionBase", "String", "Style", "TemplateContent", "TextAdapter", null, "TextDecorationCollection", "TextElement", "TextPointerWrapper",
		"TextProvider", "TextRangeAdapter", "TextRangeProvider", "Thickness", "Timeline", "TimelineMarker", "TimeSpan", "ToggleSwitchTemplateSettings", "ToolTipTemplateSettings", "Transition",
		"TransitionTarget", "TriggerAction", "TriggerBase", null, "UIElement", "UIElementClone", "VisualState", "VisualStateGroup", "VisualStateManager", "VisualTransition",
		"Window", null, null, null, null, null, null, null, null, null,
		null, null, null, null, null, null, null, null, null, null,
		null, null, null, null, null, null, null, null, null, null,
		null, null, null, null, null, null, "AddDeleteThemeTransition", "ArcSegment", "BackEase", "BeginStoryboard",
		"BezierSegment", "BindingBase", "BitmapCache", "BitmapSource", "Block", "BounceEase", "CircleEase", "ColorAnimation", "ColorAnimationUsingKeyFrames", "ContentThemeTransition",
		"ControlTemplate", "CubicEase", "CustomResource", "DataTemplate", "DiscreteColorKeyFrame", "DiscreteDoubleKeyFrame", "DiscreteObjectKeyFrame", "DiscretePointKeyFrame", "DispatcherTimer", "DoubleAnimation",
		"DoubleAnimationUsingKeyFrames", "Duration", "DynamicTimeline", "EasingColorKeyFrame", "EasingDoubleKeyFrame", "EasingPointKeyFrame", "EdgeUIThemeTransition", "ElasticEase", "EllipseGeometry", "EntranceThemeTransition",
		"EventTrigger", "ExponentialEase", "Flyout", "FrameworkElement", "FrameworkElementAutomationPeer", "GeometryGroup", "GradientBrush", "GridViewItemTemplateSettings", "GroupedDataCollectionView", "HWCompLeafNode",
		"HWCompTreeNode", "HyperlinkAutomationPeer", "Inline", "InputPaneThemeTransition", "InternalTransform", "ItemAutomationPeer", "ItemsPanelTemplate", "KeyTime", "LayoutTransitionElement", "LinearColorKeyFrame",
		"LinearDoubleKeyFrame", "LinearPointKeyFrame", "LineGeometry", "LineSegment", "ListViewItemTemplateSettings", "Matrix3DProjection", "MediaSwapChainElement", "MenuFlyout", "NullExtension", "ObjectAnimationUsingKeyFrames",
		"PaneThemeTransition", "ParallelTimeline", "PathGeometry", "PlaneProjection", "PointAnimation", "PointAnimationUsingKeyFrames", "PointerAnimationUsingKeyFrames", "PolyBezierSegment", "PolyLineSegment", "PolyQuadraticBezierSegment",
		"PopupThemeTransition", "PowerEase", "PVLStaggerFunction", "QuadraticBezierSegment", "QuadraticEase", "QuarticEase", "QuinticEase", "RectangleGeometry", "RelativeSource", "RenderTargetBitmap",
		"ReorderThemeTransition", "RepositionThemeTransition", "Setter", "SineEase", "SolidColorBrush", "SplineColorKeyFrame", "SplineDoubleKeyFrame", "SplinePointKeyFrame", "StaticResource", "SurfaceImageSource",
		"SwapChainElement", "TemplateBinding", "ThemeResource", "TileBrush", "Transform", "VectorCollectionView", "VectorViewCollectionView", null, null, null,
		"AppBarAutomationPeer", "AppBarLightDismissAutomationPeer", "AutomationPeerCollection", "AutoSuggestBoxAutomationPeer", "BitmapImage", "Border", "ButtonBaseAutomationPeer", "CaptureElement", "CaptureElementAutomationPeer", "ColorKeyFrameCollection",
		"ColumnDefinitionCollection", "ComboBoxItemAutomationPeer", "ComboBoxLightDismissAutomationPeer", "ComponentHost", "CompositeTransform", "ContentPresenter", "Control", "DatePickerAutomationPeer", "DisplayMemberTemplate", "DoubleCollection",
		"DoubleKeyFrameCollection", "DragItemThemeAnimation", "DragOverThemeAnimation", "DropTargetItemThemeAnimation", "FaceplateContentPresenterAutomationPeer", "FadeInThemeAnimation", "FadeOutThemeAnimation", "FlipViewItemAutomationPeer", "FloatCollection", "FlyoutPresenterAutomationPeer",
		"GeometryCollection", "Glyphs", "GradientStopCollection", "GroupItemAutomationPeer", "HubAutomationPeer", "HubSectionAutomationPeer", "HubSectionCollection", "HWCompMediaNode", "HWCompRenderDataNode", "HWCompSwapChainNode",
		"HWCompTreeYCbCrNode", "HWCompWebViewNode", "HWCompYCbCrTextureNode", "HWRedirectedCompTreeNode", "IconElement", "Image", "ImageAutomationPeer", "ImageBrush", "InlineUIContainer", "InputScopeNameCollection",
		"ItemsControlAutomationPeer", "ItemsPresenter", "IterableCollectionView", "LinearGradientBrush", "LineBreak", "ListBoxItemAutomationPeer", "ListViewBaseHeaderItemAutomationPeer", "ListViewBaseItemAutomationPeer", "ListViewBaseItemSecondaryChrome", "MatrixTransform",
		"MediaBase", "MediaElement", "MediaElementAutomationPeer", "MediaTransportControlsAutomationPeer", "MenuFlyoutItemAutomationPeer", "MenuFlyoutItemBaseCollection", "ObjectKeyFrameCollection", "Panel", "Paragraph", "ParametricCurveCollection",
		"ParametricCurveSegmentCollection", "PasswordBoxAutomationPeer", "PathFigureCollection", "PathSegmentCollection", "PointCollection", "PointerCollection", "PointerDownThemeAnimation", "PointerKeyFrameCollection", "PointerUpThemeAnimation", "PointKeyFrameCollection",
		"PopInThemeAnimation", "PopOutThemeAnimation", "Popup", "PopupAutomationPeer", "PopupRootAutomationPeer", "ProgressRingAutomationPeer", "RadialGradientBrush", "RangeBaseAutomationPeer", "RepeatBehavior", "RepositionThemeAnimation",
		"ResourceDictionary", "ResourceDictionaryCollection", "RichEditBoxAutomationPeer", "RichTextBlock", "RichTextBlockAutomationPeer", "RichTextBlockOverflow", "RichTextBlockOverflowAutomationPeer", "RotateTransform", "RowDefinitionCollection", "Run",
		"ScaleTransform", "ScrollViewerAutomationPeer", "SearchBoxAutomationPeer", "SelectorItemAutomationPeer", "SemanticZoomAutomationPeer", "SetterBaseCollection", "SettingsFlyoutAutomationPeer", "Shape", "SkewTransform", "Span",
		"SplitCloseThemeAnimation", "SplitOpenThemeAnimation", "Storyboard", "SwipeBackThemeAnimation", "SwipeHintThemeAnimation", "TextBlock", "TextBlockAutomationPeer", "TextBoxAutomationPeer", "TextBoxBaseAutomationPeer", "TextBoxView",
		"TextElementCollection", "ThemeAnimationBase", "ThumbAutomationPeer", "TimelineCollection", "TimelineMarkerCollection", "TimePickerAutomationPeer", "ToggleMenuFlyoutItemAutomationPeer", "ToggleSwitchAutomationPeer", "ToolTipAutomationPeer", "TransformCollection",
		"TransformGroup", "TransitionCollection", "TranslateTransform", "TriggerActionCollection", "TriggerCollection", "UIElementCollection", "Viewbox", "VirtualSurfaceImageSource", "VisualStateCollection", "VisualStateGroupCollection",
		"VisualTransitionCollection", "WebViewAutomationPeer", "WebViewBrush", "WriteableBitmap", null, null, "AppBarSeparator", "BasedOnSetterCollection", "BitmapIcon", "Bold",
		"ButtonAutomationPeer", "Canvas", "ComboBoxItemDataAutomationPeer", "CommandBarElementCollection", "ContentControl", "DatePicker", "DependencyObjectCollection", "Ellipse", "FlipViewItemDataAutomationPeer", "FontIcon",
		"FullWindowMediaRoot", "Grid", "GridViewHeaderItemAutomationPeer", "GridViewItemAutomationPeer", "Hub", "HubSection", "Hyperlink", "HyperlinkButtonAutomationPeer", "Italic", "ItemCollection",
		"ItemsControl", "Line", "ListBoxItemDataAutomationPeer", "ListViewBaseItemDataAutomationPeer", "ListViewBaseItemPresenter", "ListViewHeaderItemAutomationPeer", "ListViewItemAutomationPeer", "MediaTransportControls", "MenuFlyoutItemBase", "MenuFlyoutPresenterAutomationPeer",
		"ModernCollectionBasePanel", "PasswordBox", "Path", "PathIcon", "Polygon", "Polyline", "ProgressBarAutomationPeer", "ProgressRing", "RangeBase", "Rectangle",
		"RenderTargetBitmapRoot", "RepeatButtonAutomationPeer", "RichEditBox", "RootVisual", "ScrollBarAutomationPeer", "ScrollContentPresenter", "SearchBox", "SelectorAutomationPeer", "SemanticZoom", "SliderAutomationPeer",
		"StackPanel", "SymbolIcon", "TextBox", "TextBoxBase", "Thumb", "TickBar", "TimePicker", "ToggleButtonAutomationPeer", "ToggleSwitch", "Underline",
		"UserControl", "VariableSizedWrapGrid", "VirtualizingPanel", "WebView", "AppBar", "AppBarButtonAutomationPeer", "AppBarLightDismiss", "AppBarToggleButtonAutomationPeer", "AutoSuggestBox", "BlockCollection",
		"ButtonBase", "CarouselPanel", "CheckBoxAutomationPeer", "ComboBoxAutomationPeer", "ComboBoxLightDismiss", "ContentDialog", "FlipViewAutomationPeer", "FlyoutPresenter", "Frame", "GridViewItemDataAutomationPeer",
		"GridViewItemPresenter", "GroupItem", "InlineCollection", "ItemsStackPanel", "ItemsWrapGrid", "ListBoxAutomationPeer", "ListViewBaseAutomationPeer", "ListViewBaseHeaderItem", "ListViewItemDataAutomationPeer", "ListViewItemPresenter",
		"MenuFlyoutItem", "MenuFlyoutPresenter", "MenuFlyoutSeparator", "OrientedVirtualizingPanel", "Page", "PopupRoot", "PrintRoot", "ProgressBar", "RadioButtonAutomationPeer", "ScrollBar",
		"ScrollContentControl", "SelectorItem", "SettingsFlyout", "Slider", "SwapChainBackgroundPanel", "SwapChainPanel", "TextSelectionGripper", "ToolTip", "TransitionRoot", "Button",
		"ComboBoxItem", "CommandBar", "FlipViewItem", "GridViewAutomationPeer", "GridViewHeaderItem", "HyperlinkButton", "ListBoxItem", "ListViewAutomationPeer", "ListViewBaseItem", "ListViewHeaderItem",
		"RepeatButton", "ScrollViewer", "ToggleButton", "ToggleMenuFlyoutItem", "VirtualizingStackPanel", "WrapGrid", "AppBarButton", "AppBarToggleButton", "CheckBox", "GridViewItem",
		"ListViewItem", "RadioButton", "RootScrollViewer", "Binding", "Selector", "ComboBox", "FlipView", "ListBox", "ListViewBase", "GridView",
		"ListView", "AccessibilityView", "AlignmentX", "AlignmentY", "AnimationDirection", "AnnotationType", "AppBarClosedDisplayMode", "ApplicationTheme", "AudioCategory", "AudioDeviceType",
		"AutomationControlType", "AutomationEvents", "AutomationLiveSetting", "AutomationOrientation", "AutoSuggestionBoxTextChangeReason", "BindingMode", "BitmapCreateOptions", "BrushMappingMode", "ClickMode", "ClockState",
		null, "CollectionChange", "ColorInterpolationMode", "ComponentResourceLocation", "ContentDialogResult", "DecodePixelType", "DockPosition", "EasingMode", "EdgeTransitionLocation", "ElementCompositeMode",
		"ElementTheme", "ExpandCollapseState", "FillBehavior", "FillRule", "FlowDirection", "FlyoutPlacementMode", "FocusNavigationDirection", "FocusState", "FontCapitals", "FontEastAsianLanguage",
		"FontEastAsianWidths", "FontFraction", "FontNumeralAlignment", "FontNumeralStyle", "FontStretch", "FontStyle", "FontVariants", "GeneratorDirection", "GestureModes", "GradientSpreadMethod",
		"GridUnitType", "GroupHeaderPlacement", "HoldingState", "HorizontalAlignment", "IncrementalLoadingTrigger", "InputScopeNameValue", "ItemsUpdatingScrollMode", "KeyboardNavigationMode", "LineStackingStrategy", "ListViewReorderMode",
		"ListViewSelectionMode", "LogicalDirection", "ManipulationModes", "MarkupExtensionType", "MediaCanPlayResponse", "MediaElementState", "NavigationCacheMode", "NavigationMode", "NotifyCollectionChangedAction", "OpticalMarginAlignment",
		"Orientation", "PanelScrollingDirection", "PatternInterface", "PenLineCap", "PenLineJoin", "PlacementMode", "PointerDeviceType", "PointerDirection", "PreviewPageCountType", "PrintDocumentFormat",
		"RelativeSourceMode", "RowOrColumnMajor", "ScrollAmount", "ScrollBarVisibility", "ScrollEventType", "ScrollingIndicatorMode", "ScrollIntoViewAlignment", "ScrollMode", "SelectionMode", "SliderSnapsTo",
		"SnapPointsAlignment", "SnapPointsType", "Stereo3DVideoPackingMode", "Stereo3DVideoRenderMode", "Stretch", "StretchDirection", "StyleSimulations", "SupportedTextSelection", "SweepDirection", "Symbol",
		"SynchronizedInputType", "TextAlignment", "TextFormattingMode", "TextHintingMode", "TextLineBounds", "TextReadingOrder", "TextRenderingMode", "TextTrimming", "TextWrapping", "TickPlacement",
		"ToggleState", "TypeKind", "UpdateSourceTrigger", "VerticalAlignment", "VirtualizationMode", "VirtualKey", "VirtualKeyModifiers", "Visibility", "WindowInteractionState", "WindowVisualState",
		"ZoomMode", "ZoomUnit", null, null, null, null, null, null, null, null,
		"StoryboardCollection", "PluggableLayoutPanel", "AutomationNavigationDirection", null, null, "CalendarViewTemplateSettings", "CalendarView", "CalendarViewBaseItem", "CalendarViewDayItem", "CalendarViewItem",
		"CalendarViewDisplayMode", "CalendarViewSelectionMode", "DayOfWeek", "TileGrid", "TileGridNestedPanel", "DataPackageOperation", null, null, null, null,
		null, null, "CalendarPanel", null, null, null, "SplitViewTemplateSettings", "SplitView", "SplitViewDisplayMode", "SplitViewPanePlacement",
		"Transform3D", "CompositeTransform3D", "PerspectiveTransform3D", "AutomationActiveEnd", "AutomationAnimationStyle", "AutomationBulletStyle", "AutomationCaretBidiMode", "AutomationCaretPosition", "AutomationFlowDirections", "AutomationOutlineStyles",
		"AutomationStyleId", "AutomationTextDecorationLineStyle", "AutomationTextEditChangeType", "RelativePanel", null, "DeferredElement", "HWCompInkCanvasNode", "InkCanvas", "MenuFlyoutSubItem", "AutomationStructureChangeType",
		"PasswordRevealMode", null, "FailedMediaStreamKind", null, "TargetPropertyPath", null, "AdaptiveTrigger", "StateTriggerCollection", "HWWindowedPopupCompTreeNode", "ListViewItemPresenterCheckMode",
		"SoftwareBitmapSource", null, null, "StateTriggerBase", null, "MenuPopupThemeTransition", "StateTrigger", "WebViewExecutionMode", "WebViewSettings", "WebViewPermissionState",
		"WebViewPermissionType", "PickerFlyoutThemeTransition", "CandidateWindowAlignment", "CalendarDatePicker", "ContentDialogOpenCloseThemeTransition", "ElementCompositionPreview", "MediaTransportControlsHelper", "AutoSuggestBoxQuerySubmittedEventArgs", "AppBarTemplateSettings", "CommandBarTemplateSettings",
		"CommandBarOverflowPresenter", "DrillInThemeAnimation", "DrillOutThemeAnimation", "CalendarViewAutomationPeer", "CalendarViewBaseItemAutomationPeer", "CalendarViewDayItemAutomationPeer", "CalendarViewItemAutomationPeer", "XamlBindingHelper", "AutomationAnnotation", "AutomationPeerAnnotation",
		null, null, "AutomationAnnotationCollection", "AutomationPeerAnnotationCollection", "MenuFlyoutSubItemAutomationPeer", "SplitViewPaneAutomationPeer", "UnderlineStyle", "SplitViewLightDismissAutomationPeer", "RichEditClipboardFormat", null,
		"MenuFlyoutPresenterTemplateSettings", null, "LandmarkTargetAutomationPeer", "AutomationLandmarkType", "CalendarScrollViewerAutomationPeer", "CalendarDatePickerAutomationPeer", null, null, null, "CommandBarLabelPosition",
		null, "CommandBarDefaultLabelPosition", null, "CommandBarOverflowButtonVisibility", "HWRedirectedCompTreeNodeDComp", "HWRedirectedCompTreeNodeWinRT", "HWWindowedPopupCompTreeNodeDComp", "HWWindowedPopupCompTreeNodeWinRT", "CommandBarDynamicOverflowAction", "ConnectedAnimation",
		"ConnectedAnimationService", "LightDismissOverlayMode", "FocusVisualKind", "RequiresPointer", "ConnectedAnimationRoot", "MediaPlayer", "MediaPlayerElementAutomationPeer", "MediaPlayerPresenter", "MediaPlayerElement", "FastPlayFallbackBehaviour",
		"ElementSoundKind", "ElementSoundMode", "ElementSoundPlayerState", "FullWindowMediaRootAutomationPeer", "ApplicationRequiresPointerMode", "MediaPlaybackItemConverter", null, "BrushCollection", "CalendarViewHeaderAutomationPeer", "TextDecorations",
		"KeyTipPlacementMode", "XYFocusKeyboardNavigationMode", null, null, "AutomationComponentProperties", "ComponentPropertyValue", "XamlMarkupHelper", null, null, "ContentDialogButton",
		null, null, null, null, "XamlLight", null, "XamlLightCollection", null, "ConnectedAnimationComponent", "SvgImageSource",
		"SvgImageSourceLoadStatus", "LoadedImageSurface", "LoadedImageSourceLoadStatus", "FocusInputDeviceKind", "ComboBoxSelectionChangedTrigger", null, "XamlCompositionBrushBase", "XYFocusNavigationStrategy", "XYFocusNavigationStrategyOverride", "XamlIsland",
		"XamlIslandRootCollection", "NamedContainerAutomationPeer", "IsApiContractNotPresent", "IsApiContractPresent", "IsPropertyNotPresent", "IsPropertyPresent", "IsTypeNotPresent", "IsTypePresent", "ApplicationHighContrastAdjustment", "ElementHighContrastAdjustment",
		"WebViewElement", "MarkupExtension", null, null, "AutomationNotificationKind", "AutomationNotificationProcessing", "CharacterCasing", "DisabledFormattingAccelerators", "TextHighlighterBase", "TextRange",
		null, null, "TextHighlighter", "TextHighlighterCollection", "TextRangeCollection", "ContentDialogPlacement", "KeyboardAccelerator", null, "KeyboardAcceleratorCollection", "XamlRenderingBackgroundTask",
		"AppBarButtonTemplateSettings", "AppBarToggleButtonTemplateSettings", "MenuFlyoutItemTemplateSettings", "KeyboardAcceleratorPlacementMode", null, "ElementSpatialAudioMode", null, "FrameworkElementEx", "PanelEx", "Matrix4x4",
		"Vector3", "InteractionBase", "AutomationHeadingLevel", "ContentLinkProvider", null, "ContactContentLinkProvider", "PlaceContentLinkProvider", "ContentLinkProviderCollection", "ContentLinkChangeKind", "HandwritingView",
		"HandwritingControlView", null, "HandwritingPanelPlacementAlignment", "ContentLinkAutomationPeer", "ContentLink", "CoreCursorType", "CommandBase", "IconSource", "BitmapIconSource", "FontIconSource",
		"PathIconSource", "RelayCommand", "SymbolIconSource", "Commands", "Matrix3x2", "Vector2", "Shadow", "SmartShadow", "IconSourceElement", "CommandingContainer",
		"NullKeyedResource", null, "FlyoutShowMode", "ValidationErrorsCollection", "AppBarElementContainer", null, null, "InputValidationErrorEventAction", null, "InputValidationKind",
		"InputValidationMode", "ColorPaletteResources", "BackgroundSizing", null, "BrushTransition", null, null, "Vector3Transition", "ScalarTransition", null,
		"StandardUICommand", "StandardUICommandKind", "UIElementWeakCollection", "ThemeShadow", "Vector3TransitionComponents", "ControlHeaderPlacement", "InputValidationContext", "InputValidationCommand", "XamlUICommand", "Quaternion",
		"CaretBrowsingCaret", "ParserServiceProvider"
	};

	private static readonly string[] propertyNames = new string[2391]
	{
		"Index", "Offset", "Kind", "Name", "AcceleratorKey", "AccessibilityView", "AccessKey", "AutomationId", "ControlledPeers", "HelpText",
		"IsRequiredForForm", "ItemStatus", "ItemType", "LabeledBy", "LiveSetting", "Name", "Name", "DeferredUnlinkingPayload", "IsRecycledContainer", "ItemForItemContainer",
		"TextFormattingMode", "TextHintingMode", "TextRenderingMode", "Placement", "PlacementTarget", "ToolTipService.ToolTip", "ToolTipObject", "AnnotationAlternates", "Capitals", "CapitalSpacing",
		"CaseSensitiveForms", "ContextualAlternates", "ContextualLigatures", "ContextualSwashes", "DiscretionaryLigatures", "EastAsianExpertForms", "EastAsianLanguage", "EastAsianWidths", "Fraction", "HistoricalForms",
		"HistoricalLigatures", "Kerning", "MathematicalGreek", "NumeralAlignment", "NumeralStyle", "SlashedZero", "StandardLigatures", "StandardSwashes", "StylisticAlternates", "StylisticSet1",
		"StylisticSet10", "StylisticSet11", "StylisticSet12", "StylisticSet13", "StylisticSet14", "StylisticSet15", "StylisticSet16", "StylisticSet17", "StylisticSet18", "StylisticSet19",
		"StylisticSet2", "StylisticSet20", "StylisticSet3", "StylisticSet4", "StylisticSet5", "StylisticSet6", "StylisticSet7", "StylisticSet8", "StylisticSet9", "Variants",
		"ApplicationStarted", "RequestedTheme", "Resources", "RootVisual", "EventsSource", "SelectedItem", "Reason", "Opacity", "RelativeTransform", "Transform",
		"IsSourceGrouped", "ItemsPath", "Source", "View", "A", "B", "ContentProperty", "G", "R", "KeyTime",
		"Value", "ActualWidth", "MaxWidth", "MinWidth", "Width", "DropDownClosedHeight", "DropDownOffset", "DropDownOpenedHeight", "SelectedItemDirection", "BottomLeft",
		"BottomRight", "TopLeft", "TopRight", null, "PropertyId", "ContentProperty", "KeyTime", "Value", null, null,
		"EasingMode", "MarkupExtensionType", "NativeValue", "AttachedFlyout", "Placement", "Weight", "Template", "Bounds", "Transform", "Color",
		"Offset", "GridUnitType", "Value", "ContainerStyle", "ContainerStyleSelector", "HeaderContainerStyle", "HeaderTemplate", "HeaderTemplateSelector", "HidesIfEmpty", "Panel",
		"CandidateFontSize", "CandidateIndex", "CandidateMargin", "CandidateString", "KeyboardShortcut", "Metadata", "MetadataVisibility", "SecondaryFontSize", "ShortcutOpacity", "ShortcutVisibility",
		"Items", "PageIndex", "Width", "DesiredDeceleration", "DesiredExpansion", "DesiredDeceleration", "DesiredRotation", "DesiredDeceleration", "DesiredDisplacement", "Names",
		"NameValue", "ContentProperty", "ControlPoint1", "ControlPoint2", "Bounds", "Element", "Index", "StaggerTime", "Center", "Radius",
		"M11", "M12", "M21", "M22", "OffsetX", "OffsetY", "M11", "M12", "M13", "M14",
		"M21", "M22", "M23", "M24", "M31", "M32", "M33", "M34", "M44", "OffsetX",
		"OffsetY", "OffsetZ", "KeyTime", "Value", "SourcePageType", "CurveSegments", "BeginOffset", "ConstantCoefficient", "CubicCoefficient", "LinearCoefficient",
		"QuadraticCoefficient", "IsClosed", "IsFilled", "Segments", "StartPoint", "ContentProperty", "X", "Y", "IsInContact", "IsInRange",
		"PointerDeviceType", "PointerId", "PointerValue", "TargetValue", "KeyTime", "Value", "Count", "DesiredFormat", "DocumentSource", "PrintedPageCount",
		"ContainerAnimationEndPosition", "ContainerAnimationStartPosition", "EllipseAnimationEndPosition", "EllipseAnimationWellPosition", "EllipseDiameter", "EllipseOffset", "IndicatorLengthDelta", "EllipseDiameter", "EllipseOffset", "MaxSideLength",
		"Path", "Height", "Width", "X", "Y", "ActualHeight", "Height", "MaxHeight", "MinHeight", "Curves",
		"IsDescendant", "ShouldTargetClip", "IsSealed", "BorderBrush", "BorderThickness", "ContentTransitions", "HeaderBackground", "HeaderForeground", "IconSource", "Height",
		"Width", "Color", "ContentProperty", "BasedOn", "IsSealed", "Setters", "TargetType", "Owner", "CharacterSpacing", "FontFamily",
		"FontSize", "FontStretch", "FontStyle", "FontWeight", "Foreground", "IsTextScaleFactorEnabled", "Language", "Owner", "Bottom", "Left",
		"Right", "Top", "AutoReverse", "BeginTime", "Duration", "FillBehavior", "RepeatBehavior", "SpeedRatio", "Text", "Time",
		"Type", "Seconds", "CurtainCurrentToOffOffset", "CurtainCurrentToOnOffset", "CurtainOffToOnOffset", "CurtainOnToOffOffset", "KnobCurrentToOffOffset", "KnobCurrentToOnOffset", "KnobOffToOnOffset", "KnobOnToOffOffset",
		"FromHorizontalOffset", "FromVerticalOffset", "GeneratedStaggerFunction", "ClipTransform", "ClipTransformOrigin", "CompositeTransform", "Opacity", "Projection", "TransformOrigin", null,
		null, "AllowDrop", "CacheMode", "ChildrenInternal", "Clip", "CompositeMode", "IsDoubleTapEnabled", "IsHitTestVisible", "IsHoldingEnabled", "IsRightTapEnabled",
		"IsTapEnabled", "ManipulationMode", "Opacity", "PointerCaptures", "Projection", "RenderSize", "RenderTransform", "RenderTransformOrigin", "Transitions", "TransitionTarget",
		"UseLayoutRounding", "Visibility", "Clip", "LayoutClip", "OffsetX", "OffsetY", "Opacity", "Projection", "Transform", "TransitionTarget",
		"__DeferredStoryboard", "Storyboard", "States", "Transitions", "CustomVisualStateManager", "VisualStateGroups", "From", "GeneratedDuration", "GeneratedEasingFunction", "Storyboard",
		"To", "IsLargeArc", "Point", "RotationAngle", "Size", "SweepDirection", "Amplitude", "Storyboard", "Point1", "Point2",
		"Point3", "PixelHeight", "PixelWidth", "LineHeight", "LineStackingStrategy", "Margin", "TextAlignment", "Bounces", "Bounciness", "By",
		"EasingFunction", "EnableDependentAnimation", "From", "To", "EnableDependentAnimation", "KeyFrames", "HorizontalOffset", "VerticalOffset", "TargetType", "ResourceKey",
		"DataType", "Interval", "By", "EasingFunction", "EnableDependentAnimation", "From", "To", "EnableDependentAnimation", "KeyFrames", "TimeSpan",
		"Children", "EasingFunction", "EasingFunction", "EasingFunction", "Edge", "Oscillations", "Springiness", "Center", "RadiusX", "RadiusY",
		"FromHorizontalOffset", "FromVerticalOffset", "IsStaggeringEnabled", "Actions", "RoutedEvent", "Exponent", "Content", "FlyoutPresenterStyle", "ActualHeight", "ActualWidth",
		"DataContext", "FlowDirection", "Height", "HorizontalAlignment", "IsTextScaleFactorEnabledInternal", "Language", "Margin", "MaxHeight", "MaxWidth", "MinHeight",
		"MinWidth", "Parent", "RequestedTheme", "Resources", "Style", "Tag", "Triggers", "VerticalAlignment", "Width", "Owner",
		"Children", "FillRule", "ColorInterpolationMode", "GradientStops", "MappingMode", "SpreadMethod", "DragItemsCount", "TextDecorations", "Item", "ItemsControlAutomationPeer",
		"TimeSpan", "EndPoint", "StartPoint", "Point", "DragItemsCount", "ProjectionMatrix", "Items", "MenuFlyoutPresenterStyle", "EnableDependentAnimation", "KeyFrames",
		"Edge", "Figures", "FillRule", "CenterOfRotationX", "CenterOfRotationY", "CenterOfRotationZ", "GlobalOffsetX", "GlobalOffsetY", "GlobalOffsetZ", "LocalOffsetX",
		"LocalOffsetY", "LocalOffsetZ", "ProjectionMatrix", "RotationX", "RotationY", "RotationZ", "By", "EasingFunction", "EnableDependentAnimation", "From",
		"To", "EnableDependentAnimation", "KeyFrames", "KeyFrames", "PointerSource", "Points", "Points", "Points", "FromHorizontalOffset", "FromVerticalOffset",
		"Power", "Delay", "DelayReduce", "Maximum", "Reverse", "Point1", "Point2", "RadiusX", "RadiusY", "Rect",
		"Mode", "PixelHeight", "PixelWidth", "Property", "Value", "Color", "KeySpline", "KeySpline", "KeySpline", "ResourceKey",
		"Property", "ResourceKey", "AlignmentX", "AlignmentY", "Stretch", "ContentProperty", "Converter", "ConverterLanguage", "ConverterParameter", "ElementName",
		"FallbackValue", "Mode", "Path", "RelativeSource", "Source", "TargetNullValue", "UpdateSourceTrigger", "CreateOptions", "DecodePixelHeight", "DecodePixelType",
		"DecodePixelWidth", "UriSource", "Background", "BorderBrush", "BorderThickness", "Child", "ChildTransitions", "CornerRadius", "Padding", "Source",
		"Stretch", "ContentProperty", "ContentProperty", "CenterX", "CenterY", "Rotation", "ScaleX", "ScaleY", "SkewX", "SkewY",
		"TranslateX", "TranslateY", "CharacterSpacing", "Content", "ContentTemplate", "ContentTemplateSelector", "ContentTransitions", "FontFamily", "FontSize", "FontStretch",
		"FontStyle", "FontWeight", "Foreground", "IsTextScaleFactorEnabled", "LineStackingStrategy", "MaxLines", "OpticalMarginAlignment", "SelectedContentTemplate", "TextLineBounds", "TextWrapping",
		"Background", "BorderBrush", "BorderThickness", "CharacterSpacing", "DefaultStyleKey", "FocusState", "FontFamily", "FontSize", "FontStretch", "FontStyle",
		"FontWeight", "Foreground", "HorizontalContentAlignment", "IsEnabled", "IsTabStop", "IsTextScaleFactorEnabled", "Padding", "TabIndex", "TabNavigation", "Template",
		"VerticalContentAlignment", "DisplayMemberPath", "ContentProperty", "ContentProperty", "TargetName", "Direction", "TargetName", "ToOffset", "TargetName", "TargetName",
		"TargetName", "ContentProperty", "ContentProperty", "Fill", "FontRenderingEmSize", "FontUri", "Indices", "OriginX", "OriginY", "StyleSimulations",
		"UnicodeString", "ContentProperty", "ContentProperty", "Foreground", "DownloadProgress", "NineGrid", "PlayToSource", "Source", "Stretch", "DownloadProgress",
		"ImageSource", "Child", "ContentProperty", "Footer", "FooterTemplate", "FooterTransitions", "Header", "HeaderTemplate", "HeaderTransitions", "ItemsPanel",
		"Padding", "EndPoint", "StartPoint", "Matrix", "ActualStereo3DVideoPackingMode", "AreTransportControlsEnabled", "AspectRatioHeight", "AspectRatioWidth", "AudioCategory", "AudioDeviceType",
		"AudioStreamCount", "AudioStreamIndex", "AutoPlay", "Balance", "BufferingProgress", "CanPause", "CanSeek", "CurrentState", "DefaultPlaybackRate", "DownloadProgress",
		"DownloadProgressOffset", "FullScreen", "IsAudioOnly", "IsFullWindow", "IsLooping", "IsMuted", "IsStereo3DVideo", "Markers", "NaturalDuration", "NaturalVideoHeight",
		"NaturalVideoWidth", "PlaybackRate", "PlayToPreferredSourceUri", "PlayToSource", "Position", "PosterSource", "ProtectionManager", "RealTimePlayback", "Source", "Stereo3DVideoPackingMode",
		"Stereo3DVideoRenderMode", "Stretch", "TransportControls", "Volume", "ContentProperty", "ContentProperty", "Background", "Children", "ChildrenTransitions", "IsIgnoringTransitions",
		"IsItemsHost", "Inlines", "TextIndent", "ContentProperty", "ContentProperty", "ContentProperty", "ContentProperty", "ContentProperty", "ContentProperty", "TargetName",
		"ContentProperty", "TargetName", "ContentProperty", "FromHorizontalOffset", "FromVerticalOffset", "TargetName", "TargetName", "Child", "ChildTransitions", "HorizontalOffset",
		"IsApplicationBarService", "IsFlyout", "IsLightDismissEnabled", "IsOpen", "IsSettingsFlyout", "VerticalOffset", "Center", "GradientOrigin", "RadiusX", "RadiusY",
		"Count", "Duration", "FromHorizontalOffset", "FromVerticalOffset", "TargetName", "ContentProperty", "MergedDictionaries", "Source", "ThemeDictionaries", "ContentProperty",
		"Blocks", "CharacterSpacing", "FontFamily", "FontSize", "FontStretch", "FontStyle", "FontWeight", "Foreground", "HasOverflowContent", "IsColorFontEnabled",
		"IsTextScaleFactorEnabled", "IsTextSelectionEnabled", "LineHeight", "LineStackingStrategy", "MaxLines", "OpticalMarginAlignment", "OverflowContentTarget", "Padding", "SelectedText", "SelectionHighlightColor",
		"TextAlignment", "TextIndent", "TextLineBounds", "TextReadingOrder", "TextTrimming", "TextWrapping", "HasOverflowContent", "MaxLines", "OverflowContentTarget", "Padding",
		"Angle", "CenterX", "CenterY", "ContentProperty", "FlowDirection", "Text", "CenterX", "CenterY", "ScaleX", "ScaleY",
		"ContentProperty", "IsSealed", "Fill", "GeometryTransform", "Stretch", "Stroke", "StrokeDashArray", "StrokeDashCap", "StrokeDashOffset", "StrokeEndLineCap",
		"StrokeLineJoin", "StrokeMiterLimit", "StrokeStartLineCap", "StrokeThickness", "AngleX", "AngleY", "CenterX", "CenterY", "Inlines", "ClosedLength",
		"ClosedTarget", "ClosedTargetName", "ContentTarget", "ContentTargetName", "ContentTranslationDirection", "ContentTranslationOffset", "OffsetFromCenter", "OpenedLength", "OpenedTarget", "OpenedTargetName",
		"ClosedLength", "ClosedTarget", "ClosedTargetName", "ContentTarget", "ContentTargetName", "ContentTranslationDirection", "ContentTranslationOffset", "OffsetFromCenter", "OpenedLength", "OpenedTarget",
		"OpenedTargetName", "Children", "IsEssential", "TargetName", "TargetProperty", "FromHorizontalOffset", "FromVerticalOffset", "TargetName", "TargetName", "ToHorizontalOffset",
		"ToVerticalOffset", "CharacterSpacing", "FontFamily", "FontSize", "FontStretch", "FontStyle", "FontWeight", "Foreground", "Inlines", "IsColorFontEnabled",
		"IsTextScaleFactorEnabled", "IsTextSelectionEnabled", "LineHeight", "LineStackingStrategy", "MaxLines", "OpticalMarginAlignment", "Padding", "SelectedText", "SelectionHighlightColor", "Text",
		"TextAlignment", "TextDecorations", "TextLineBounds", "TextReadingOrder", "TextTrimming", "TextWrapping", "ContentProperty", "ContentProperty", "ContentProperty", "ContentProperty",
		"Children", "Value", "ContentProperty", "X", "Y", "ContentProperty", "ContentProperty", "ContentProperty", "Child", "Stretch",
		"StretchDirection", "ContentProperty", "ContentProperty", "ContentProperty", "SourceName", "IsCompact", "UriSource", "Left", "Top", "ZIndex",
		"ContentProperty", "Content", "ContentTemplate", "ContentTemplateSelector", "ContentTransitions", "SelectedContentTemplate", "CalendarIdentifier", "Date", "DayFormat", "DayVisible",
		"Header", "HeaderTemplate", "MaxYear", "MinYear", "MonthFormat", "MonthVisible", "Orientation", "YearFormat", "YearVisible", "ContentProperty",
		"FontFamily", "FontSize", "FontStyle", "FontWeight", "Glyph", "IsTextScaleFactorEnabled", "Column", "ColumnDefinitions", "ColumnSpan", "Row",
		"RowDefinitions", "RowSpan", "DefaultSectionIndex", "Header", "HeaderTemplate", "IsActiveView", "IsZoomedInView", "Orientation", "SectionHeaders", "Sections",
		"SectionsInView", "SemanticZoomOwner", "ContentTemplate", "Header", "HeaderTemplate", "IsHeaderInteractive", "NavigateUri", "ContentProperty", "DisplayMemberPath", "GroupStyle",
		"GroupStyleSelector", "IsGrouping", "IsItemsHostInvalid", "ItemContainerStyle", "ItemContainerStyleSelector", "ItemContainerTransitions", "Items", "ItemsHost", "ItemsPanel", "ItemsSource",
		"ItemTemplate", "ItemTemplateSelector", "X1", "X2", "Y1", "Y2", null, "IsFastForwardButtonVisible", null, "IsFastRewindButtonVisible",
		null, "IsFullWindowButtonVisible", null, "IsPlaybackRateButtonVisible", "IsSeekBarVisible", null, null, "IsStopButtonVisible", null, "IsVolumeButtonVisible",
		null, "IsZoomButtonVisible", "Header", "HeaderTemplate", "IsPasswordRevealButtonEnabled", "MaxLength", "Password", "PasswordChar", "PlaceholderText", "PreventKeyboardDisplayOnProgrammaticFocus",
		"SelectionHighlightColor", "Data", "Data", "FillRule", "Points", "FillRule", "Points", "IsActive", "TemplateSettings", "LargeChange",
		"Maximum", "Minimum", "SmallChange", "Value", "RadiusX", "RadiusY", "AcceptsReturn", "Header", "HeaderTemplate", "InputScope",
		"IsColorFontEnabled", "IsReadOnly", "IsSpellCheckEnabled", "IsTextPredictionEnabled", "PlaceholderText", "PreventKeyboardDisplayOnProgrammaticFocus", "SelectionHighlightColor", "TextAlignment", "TextWrapping", "ChooseSuggestionOnEnter",
		"FocusOnKeyboardInput", "PlaceholderText", "QueryText", "SearchHistoryContext", "SearchHistoryEnabled", "CanChangeViews", "IsZoomedInViewActive", "IsZoomOutButtonEnabled", "ZoomedInView", "ZoomedOutView",
		"AreScrollSnapPointsRegular", "Orientation", "Symbol", "AcceptsReturn", "Header", "HeaderTemplate", "InputScope", "IsColorFontEnabled", "IsCoreDesktopPopupMenuEnabled", null,
		"IsReadOnly", "IsSpellCheckEnabled", "IsTextPredictionEnabled", "MaxLength", "PlaceholderText", "PreventKeyboardDisplayOnProgrammaticFocus", "SelectedText", "SelectionHighlightColor", "SelectionLength", "SelectionStart",
		"Text", "TextAlignment", "TextWrapping", "IsDragging", "Fill", "ClockIdentifier", "Header", "HeaderTemplate", "MinuteIncrement", "Time",
		"Header", "HeaderTemplate", "IsOn", "OffContent", "OffContentTemplate", "OnContent", "OnContentTemplate", "TemplateSettings", "Content", "ColumnSpan",
		"HorizontalChildrenAlignment", "ItemHeight", "ItemWidth", "MaximumRowsOrColumns", "Orientation", "RowSpan", "VerticalChildrenAlignment", "AllowedScriptNotifyUris", "CanGoBack", "CanGoForward",
		"ContainsFullScreenElement", "DataTransferPackage", "DefaultBackgroundColor", "DocumentTitle", "Source", "ClosedDisplayMode", "IsOpen", "IsSticky", "AutoMaximizeSuggestionArea", "Header",
		"IsSuggestionListOpen", "MaxSuggestionListHeight", "PlaceholderText", "Text", "TextBoxStyle", "TextMemberPath", "UpdateTextOnSelect", "ContentProperty", "ClickMode", "Command",
		"CommandParameter", "IsPointerOver", "IsPressed", "FullSizeDesired", "IsPrimaryButtonEnabled", "IsSecondaryButtonEnabled", "PrimaryButtonCommand", "PrimaryButtonCommandParameter", "PrimaryButtonText", "SecondaryButtonCommand",
		"SecondaryButtonCommandParameter", "SecondaryButtonText", "Title", "TitleTemplate", "BackStack", "BackStackDepth", "CacheSize", "CanGoBack", "CanGoForward", "CurrentSourcePageType",
		"ForwardStack", "SourcePageType", "CheckBrush", "CheckHintBrush", "CheckSelectingBrush", "ContentMargin", "DisabledOpacity", "DragBackground", "DragForeground", "DragOpacity",
		"FocusBorderBrush", "GridViewItemPresenterHorizontalContentAlignment", "GridViewItemPresenterPadding", "PlaceholderBackground", "PointerOverBackground", "PointerOverBackgroundMargin", "ReorderHintOffset", "SelectedBackground", "SelectedBorderThickness", "SelectedForeground",
		"SelectedPointerOverBackground", "SelectedPointerOverBorderBrush", "SelectionCheckMarkVisualEnabled", "GridViewItemPresenterVerticalContentAlignment", "ContentProperty", "CacheLength", "GroupHeaderPlacement", "GroupPadding", "ItemsUpdatingScrollMode", "Orientation",
		"CacheLength", "GroupHeaderPlacement", "GroupPadding", "ItemHeight", "ItemWidth", "MaximumRowsOrColumns", "Orientation", "CheckBrush", "CheckHintBrush", "CheckSelectingBrush",
		"ContentMargin", "DisabledOpacity", "DragBackground", "DragForeground", "DragOpacity", "FocusBorderBrush", "ListViewItemPresenterHorizontalContentAlignment", "ListViewItemPresenterPadding", "PlaceholderBackground", "PointerOverBackground",
		"PointerOverBackgroundMargin", "ReorderHintOffset", "SelectedBackground", "SelectedBorderThickness", "SelectedForeground", "SelectedPointerOverBackground", "SelectedPointerOverBorderBrush", "SelectionCheckMarkVisualEnabled", "ListViewItemPresenterVerticalContentAlignment", "Command",
		"CommandParameter", "Text", "IsContainerGeneratedForInsert", "BottomAppBar", "Frame", "NavigationCacheMode", "TopAppBar", "IsIndeterminate", "ShowError", "ShowPaused",
		"TemplateSettings", "IndicatorMode", "Orientation", "ViewportSize", "IsSelectionActive", "IsSynchronizedWithCurrentItem", "SelectedIndex", "SelectedItem", "SelectedValue", "SelectedValuePath",
		"IsSelected", "HeaderBackground", "HeaderForeground", "IconSource", "TemplateSettings", "Title", "Header", "HeaderTemplate", "IntermediateValue", "IsDirectionReversed",
		"IsThumbToolTipEnabled", "Orientation", "SnapsTo", "StepFrequency", "ThumbToolTipValueConverter", "TickFrequency", "TickPlacement", "CompositionScaleX", "CompositionScaleY", "HorizontalOffset",
		"IsOpen", "Placement", "PlacementTarget", "TemplateSettings", "VerticalOffset", "Flyout", "Header", "HeaderTemplate", "IsDropDownOpen", "IsEditable",
		"IsSelectionBoxHighlighted", "MaxDropDownHeight", "PlaceholderText", "SelectionBoxItem", "SelectionBoxItemTemplate", "TemplateSettings", "PrimaryCommands", "SecondaryCommands", "UseTouchAnimationsForAllNavigation", "NavigateUri",
		"SelectedItems", "SelectionMode", "CanDragItems", "CanReorderItems", "DataFetchSize", "Footer", "FooterTemplate", "FooterTransitions", "Header", "HeaderTemplate",
		"HeaderTransitions", "IncrementalLoadingThreshold", "IncrementalLoadingTrigger", "IsActiveView", "IsItemClickEnabled", "IsSwipeEnabled", "IsZoomedInView", "ReorderMode", "SelectedItems", "SelectionMode",
		"SemanticZoomOwner", "ShowsScrollingPlaceholders", "Delay", "Interval", "BringIntoViewOnFocusChange", "ComputedHorizontalScrollBarVisibility", "ComputedVerticalScrollBarVisibility", "ExtentHeight", "ExtentWidth", "HorizontalOffset",
		"HorizontalScrollBarVisibility", "HorizontalScrollMode", "HorizontalSnapPointsAlignment", "HorizontalSnapPointsType", "IsDeferredScrollingEnabled", "IsHorizontalRailEnabled", "IsHorizontalScrollChainingEnabled", "IsScrollInertiaEnabled", "IsVerticalRailEnabled", "IsVerticalScrollChainingEnabled",
		"IsZoomChainingEnabled", "IsZoomInertiaEnabled", "LeftHeader", "MaxZoomFactor", "MinZoomFactor", "ScrollableHeight", "ScrollableWidth", "TopHeader", "TopLeftHeader", "VerticalOffset",
		"VerticalScrollBarVisibility", "VerticalScrollMode", "VerticalSnapPointsAlignment", "VerticalSnapPointsType", "ViewportHeight", "ViewportWidth", "ZoomFactor", "ZoomMode", "ZoomSnapPoints", "ZoomSnapPointsType",
		"IsChecked", "IsThreeState", "IsChecked", "AreScrollSnapPointsRegular", "IsContainerGeneratedForInsert", "IsVirtualizing", "Orientation", "VirtualizationMode", "HorizontalChildrenAlignment", "ItemHeight",
		"ItemWidth", "MaximumRowsOrColumns", "Orientation", "VerticalChildrenAlignment", "Icon", "IsCompact", "Label", "Icon", "IsCompact", "Label",
		"TemplateSettings", "TemplateSettings", "GroupName", "ActiveStoryboards", "ActiveTransitions", null, null, null, null, null,
		null, null, null, null, "ContentProperty", "CacheLength", "ColorFontPaletteIndex", "IsColorFontEnabled", null, null,
		null, null, null, "HasMoreContentAfter", "HasMoreContentBefore", "HasMoreViews", "HeaderText", null, null, "WeekDay1",
		"WeekDay2", "WeekDay3", "WeekDay4", "WeekDay5", "WeekDay6", "WeekDay7", null, null, null, null,
		"CalendarIdentifier", null, null, null, null, null, null, null, "DayOfWeekFormat", null,
		null, "DisplayMode", "FirstDayOfWeek", null, null, null, null, null, null, null,
		null, null, null, null, null, null, "IsOutOfScopeEnabled", "IsTodayHighlighted", null, "MaxDate",
		"MinDate", null, null, null, null, null, "NumberOfWeeksInView", null, null, null,
		null, null, "SelectedDates", null, "SelectionMode", "TemplateSettings", null, null, "Date", "IsBlackout",
		"Date", null, null, null, null, null, null, null, null, null,
		null, null, null, null, null, null, null, null, null, null,
		null, null, null, null, null, null, null, null, null, null,
		null, null, null, null, null, null, null, null, null, null,
		null, "IsFastForwardEnabled", "IsFastRewindEnabled", "IsFullWindowEnabled", "IsPlaybackRateEnabled", "IsSeekEnabled", "IsStopEnabled", "IsVolumeEnabled", "IsZoomEnabled", null,
		"Column", "Line", null, null, "OffsetXAnimation", "OffsetYAnimation", "CenterXAnimation", "CenterYAnimation", "RotateAnimation", "ScaleXAnimation",
		"ScaleYAnimation", "SkewXAnimation", "SkewYAnimation", "TranslateXAnimation", "TranslateYAnimation", "AngleAnimation", "CenterXAnimation", "CenterYAnimation", "CenterXAnimation", "CenterYAnimation",
		"ScaleXAnimation", "ScaleYAnimation", "AngleXAnimation", "AngleYAnimation", "CenterXAnimation", "CenterYAnimation", "XAnimation", "YAnimation", null, null,
		null, null, null, null, "LineHeight", "UseOverflowStyle", "UseOverflowStyle", "UseOverflowStyle", "CacheLength", "Cols",
		"Orientation", "Rows", "ItemMinHeight", "ItemMinWidth", "MinViewWidth", null, null, null, null, null,
		null, "OpacityAnimation", "OpacityAnimation", "CenterOfRotationXAnimation", "CenterOfRotationYAnimation", "CenterOfRotationZAnimation", "GlobalOffsetXAnimation", "GlobalOffsetYAnimation", "GlobalOffsetZAnimation", "LocalOffsetXAnimation",
		"LocalOffsetYAnimation", "LocalOffsetZAnimation", "RotationXAnimation", "RotationYAnimation", "RotationZAnimation", null, "CacheLength", "Orientation", "SelectedRanges", null,
		null, "CompactPaneGridLength", "NegativeOpenPaneLength", "NegativeOpenPaneLengthMinusCompactLength", "OpenPaneGridLength", "OpenPaneLengthMinusCompactLength", "CompactPaneLength", "Content", "DisplayMode", "IsPaneOpen",
		"OpenPaneLength", "Pane", "PanePlacement", "TemplateSettings", "Transform3D", "CenterX", "CenterXAnimation", "CenterY", "CenterYAnimation", "CenterZ",
		"CenterZAnimation", "RotationX", "RotationXAnimation", "RotationY", "RotationYAnimation", "RotationZ", "RotationZAnimation", "ScaleX", "ScaleXAnimation", "ScaleY",
		"ScaleYAnimation", "ScaleZ", "ScaleZAnimation", "TranslateX", "TranslateXAnimation", "TranslateY", "TranslateYAnimation", "TranslateZ", "TranslateZAnimation", "Depth",
		"OffsetX", "OffsetY", "ColorAAnimation", "ColorBAnimation", "ColorGAnimation", "ColorRAnimation", "ParseUri", "Above", "AlignBottomWith", "AlignLeftWith",
		null, null, null, null, "AlignRightWith", "AlignTopWith", "Below", null, null, "LeftOf",
		"RightOf", null, null, "OpenPaneLength", "TransparentBackground", "AreStickyGroupHeadersEnabledBase", "PasswordRevealMode", "PaneBackground", "AreStickyGroupHeadersEnabled", "AreStickyGroupHeadersEnabled",
		"Items", "Text", "XbfHash", "CanDrag", "ExtensionInstance", null, null, null, null, null,
		null, null, null, null, null, null, null, null, null, null,
		null, "AlignHorizontalCenterWith", "AlignVerticalCenterWith", null, "Path", "Target", "__DeferredSetters", "Setters", "StateTriggers", "MinWindowHeight",
		"MinWindowWidth", "Target", "ContentProperty", null, "BlackoutForeground", "CalendarItemBackground", "CalendarItemBorderBrush", "CalendarItemBorderThickness", "CalendarItemForeground", "CalendarViewDayItemStyle",
		"DayItemFontFamily", "DayItemFontSize", "DayItemFontStyle", "DayItemFontWeight", "FirstOfMonthLabelFontFamily", "FirstOfMonthLabelFontSize", "FirstOfMonthLabelFontStyle", "FirstOfMonthLabelFontWeight", "FirstOfYearDecadeLabelFontFamily", "FirstOfYearDecadeLabelFontSize",
		"FirstOfYearDecadeLabelFontStyle", "FirstOfYearDecadeLabelFontWeight", "FocusBorderBrush", "HorizontalDayItemAlignment", "HorizontalFirstOfMonthLabelAlignment", "HoverBorderBrush", null, "MonthYearItemFontFamily", "MonthYearItemFontSize", "MonthYearItemFontStyle",
		"MonthYearItemFontWeight", "OutOfScopeBackground", "OutOfScopeForeground", "PressedBorderBrush", "PressedForeground", "SelectedBorderBrush", "SelectedForeground", "SelectedHoverBorderBrush", "SelectedPressedBorderBrush", "TodayFontWeight",
		"TodayForeground", "VerticalDayItemAlignment", "VerticalFirstOfMonthLabelAlignment", null, "IsCompact", "AlignBottomWithPanel", "AlignHorizontalCenterWithPanel", "AlignLeftWithPanel", "AlignRightWithPanel", "AlignTopWithPanel",
		"AlignVerticalCenterWithPanel", "IsMultiSelectCheckBoxEnabled", "IsDraggable", "Level", "PositionInSet", "SizeOfSet", "CheckBoxBrush", "CheckMode", null, "PressedBackground",
		"SelectedPressedBackground", "FocusTargetDescendant", "IsTemplateFocusTarget", "UseSystemFocusVisuals", null, null, "DirectManipulationContainer", "FocusSecondaryBorderBrush", null, "PointerOverForeground",
		"MirroredWhenRightToLeft", "CenterX", "CenterY", "ClipRect", "HandOffCompositionVisual", "DeferredStorage", "RealizingProxy", "CanvasOffset", null, null,
		null, null, null, "ClosedRatio", "Direction", "OpenedLength", "DeferredStateTriggers", "TriggerState", null, "TextReadingOrder",
		"TextReadingOrder", "TextReadingOrder", "ExecutionMode", "CachedStyleSetterProperty", "DeferredPermissionRequests", "Settings", "OffsetFromCenter", "OpenedLength", null, "DesiredCandidateWindowAlignment",
		null, "DesiredCandidateWindowAlignment", "CalendarIdentifier", "CalendarViewStyle", "Date", "DateFormat", "DayOfWeekFormat", "DisplayMode", "FirstDayOfWeek", "Header",
		"HeaderTemplate", "IsCalendarOpen", "IsGroupLabelVisible", "IsOutOfScopeEnabled", "IsTodayHighlighted", "MaxDate", "MinDate", "PlaceholderText", "IsGroupLabelVisible", "Background",
		"BorderBrush", "BorderThickness", "CornerRadius", "Padding", "BorderBrush", "BorderThickness", "CornerRadius", "Padding", "BorderBrush", "BorderThickness",
		"CornerRadius", "Padding", "BorderBrush", "BorderThickness", "CornerRadius", "Padding", "InputScope", "DropoutOrder", "ChosenSuggestion", "QueryText",
		"QueryIcon", "IsActive", "HorizontalContentAlignment", "VerticalContentAlignment", "ClipRect", "CompactRootMargin", "CompactVerticalDelta", "HiddenRootMargin", "HiddenVerticalDelta", "MinimalRootMargin",
		"MinimalVerticalDelta", "ContentHeight", "NegativeOverflowContentHeight", "OverflowContentClipRect", "OverflowContentHeight", "OverflowContentHorizontalOffset", "OverflowContentMaxHeight", "OverflowContentMinWidth", "TemplateSettings", "CommandBarOverflowPresenterStyle",
		"CommandBarTemplateSettings", "EntranceTarget", "EntranceTargetName", "ExitTarget", "ExitTargetName", "EntranceTarget", "EntranceTargetName", "ExitTarget", "ExitTargetName", "DataTemplateComponent",
		"DeferredSetters", "Annotations", "Element", "Type", "Peer", "Type", "ContentProperty", "ContentProperty", "StartIndex", "AutomationPeerFactoryIndex",
		"UnderlineStyle", "DisabledForeground", "TodayBackground", "TodayBlackoutBackground", "TodayHoverBorderBrush", "TodayPressedBorderBrush", "TodaySelectedInnerBorderBrush", "IsContentDialog", "IsFocusEngaged", null,
		"ElevatorHelper", "IsFocusEngagementEnabled", "LayoutToWindowBounds", "ClipboardCopyFormat", "PreventEditFocusLoss", "HandInCompositionVisual", "OverflowContentMaxWidth", "DropDownContentMinWidth", null, null,
		"HandOffVisualTransform", "FlyoutContentMinWidth", "TemplateSettings", null, null, "LandmarkType", "LocalizedLandmarkType", "GlobalScaleFactor", "IsStaggeringEnabled", "SingleSelectionFollowsFocus",
		"SingleSelectionFollowsFocus", "AssociatedFlyout", "AutoPlay", "IsAnimatedBitmap", "IsPlaying", "FullDescription", "IsDataValidForForm", "IsPeripheral", "LocalizedControlType", "AllowFocusOnInteraction",
		"AllowFocusOnInteraction", "AllowFocusOnInteraction", "RequiresPointer", null, "ContextFlyout", "AccessKey", "AccessKeyScopeOwner", "IsAccessKeyScope", null, "DescribedBy",
		null, null, null, null, null, null, null, null, null, null,
		null, null, "AccessKey", "XYFocusDown", "XYFocusLeft", "XYFocusRight", "XYFocusUp", "XYFocusDown", "XYFocusLeft", "XYFocusRight",
		"XYFocusUp", "XYFocusDown", "XYFocusLeft", "XYFocusRight", "XYFocusUp", "EffectiveOverflowButtonVisibility", "IsInOverflow", "DefaultLabelPosition", "IsDynamicOverflowEnabled", "OverflowButtonVisibility",
		"IsInOverflow", "LabelPosition", "IsInOverflow", "LabelPosition", "LightDismissOverlayMode", "DisableOverlayIsLightDismissCheck", "LightDismissOverlayMode", "OverlayElement", "LightDismissOverlayMode", "LightDismissOverlayMode",
		"LightDismissOverlayMode", "LightDismissOverlayMode", "LightDismissOverlayMode", "LightDismissOverlayMode", "LightDismissOverlayMode", "DynamicOverflowOrder", "DynamicOverflowOrder", "DynamicOverflowOrder", "FocusVisualMargin", "FocusVisualPrimaryBrush",
		"FocusVisualPrimaryThickness", "FocusVisualSecondaryBrush", "FocusVisualSecondaryThickness", null, null, "AllowFocusWhenDisabled", "AllowFocusWhenDisabled", "IsTextSearchEnabled", "ExitDisplayModeOnAccessKeyInvoked", "ExitDisplayModeOnAccessKeyInvoked",
		"IsFullWindow", "MediaPlayer", "Stretch", "AreTransportControlsEnabled", "AutoPlay", "IsFullWindow", "MediaPlayer", "PosterSource", "Source", "Stretch",
		"TransportControls", "FastPlayFallbackBehaviour", "IsNextTrackButtonVisible", "IsPreviousTrackButtonVisible", "IsSkipBackwardButtonVisible", "IsSkipBackwardEnabled", "IsSkipForwardButtonVisible", "IsSkipForwardEnabled", "ElementSoundMode", "ElementSoundMode",
		"ElementSoundMode", "OpacityExpression", "IsGamepadFocusCandidate", "IsSubMenu", "ContentProperty", "FlowsFrom", "FlowsTo", "RequiresPointerMode", "TextDecorations", "ColorAnimation",
		"TextDecorations", "DefaultStyleResourceUri", null, "PrimaryButtonStyle", "SecondaryButtonStyle", null, null, null, null, "KeyTipHorizontalOffset",
		"KeyTipPlacementMode", "KeyTipVerticalOffset", "KeyTipHorizontalOffset", "KeyTipPlacementMode", "KeyTipVerticalOffset", "OverlayInputPassThroughElement", "XYFocusKeyboardNavigation", "Culture", "HandOffVisualClip", null,
		null, null, null, null, null, null, null, null, null, null,
		null, null, null, null, null, null, null, "XYFocusDownNavigationStrategy", "XYFocusLeftNavigationStrategy", "XYFocusRightNavigationStrategy",
		"XYFocusUpNavigationStrategy", "XYFocusDownNavigationStrategy", "XYFocusLeftNavigationStrategy", "XYFocusRightNavigationStrategy", "XYFocusUpNavigationStrategy", "AccessKeyScopeOwner", "IsAccessKeyScope", "ControlledPeers", "DescribedBy", "LabeledBy",
		"ElementId", "Site", "HandOffVisualTransformMatrix3D", "FocusState", null, "CloseButtonCommand", "CloseButtonCommandParameter", "CloseButtonStyle", "CloseButtonText", "DefaultButton",
		"SelectionHighlightColorWhenNotFocused", "SelectionHighlightColorWhenNotFocused", null, null, null, "ContentProperty", null, "RasterizePixelHeight", "RasterizePixelWidth", "UriSource",
		null, null, null, null, "DecodedPhysicalSize", "DecodedSize", "NaturalSize", "SelectionChangedTrigger", null, "FallbackColor",
		"Size", "Lights", "Icon", "Icon", "ShowAsMonochrome", "HighContrastAdjustment", "HighContrastAdjustment", "MaxLength", "TabFocusNavigation", "IsTemplateKeyTipTarget",
		"TemplateKeyTipTarget", "IsTabStop", "TabIndex", "IsRepeatButtonVisible", "IsRepeatEnabled", "ShowAndHideAutomatically", "DisabledFormattingAccelerators", "CharacterCasing", "CharacterCasing", "IsTextTrimmed",
		"IsTextTrimmed", "IsTextTrimmed", "Length", "StartIndex", "Background", "Foreground", "Ranges", "TextHighlighters", "TextHighlighters", "ContentProperty",
		"ContentProperty", "ActualTheme", "ColumnSpacing", "RowSpacing", "Spacing", "HorizontalTextAlignment", "HorizontalTextAlignment", "HorizontalTextAlignment", "HorizontalTextAlignment", "HorizontalTextAlignment",
		"PlaceholderForeground", "PlaceholderForeground", "IsEnabled", "Key", "Modifiers", "ScopeOwner", "KeyboardAccelerators", "ContentProperty", "RevealBackground", "RevealBackgroundShowsAboveContent",
		"RevealBorderBrush", "RevealBorderThickness", null, "KeyTipTarget", "KeyboardAcceleratorTextMinWidth", "KeyboardAcceleratorTextMinWidth", "KeyboardAcceleratorTextMinWidth", null, "TemplateSettings", null,
		"TemplateSettings", null, "TemplateSettings", "BringIntoViewDistanceX", "BringIntoViewDistanceY", "EffectiveViewport", "MaxViewport", "KeyboardAcceleratorPlacementMode", null, null,
		"IsResponsiveToOcclusions", "IsCompactOverlayButtonVisible", "IsCompactOverlayEnabled", null, "ActualHeight", "ActualWidth", "Children", "Height", "HorizontalAlignment", "Margin",
		"MaxHeight", "MaxWidth", "MinHeight", "MinWidth", "VerticalAlignment", "Width", "ActualHeight", "ActualWidth", "Children", "Height",
		"HorizontalAlignment", "Margin", "MaxHeight", "MaxWidth", "MinHeight", "MinWidth", "VerticalAlignment", "Width", "KeyboardAcceleratorToolTip", "KeyboardAcceleratorToolTipObject",
		"KeyboardAcceleratorPlacementTarget", "CenterPoint", "Rotation", "RotationAxis", "Scale", "TransformMatrix", "Translation", "HandwritingView", "HeadingLevel", null,
		null, null, null, null, null, "IsHandwritingViewEnabled", "ContentProperty", "ContentLinkProviders", "ContentLinkBackgroundColor", "ContentLinkForegroundColor",
		"AreCandidatesEnabled", "IsOpen", null, "PlacementTarget", "PlacementAlignment", "HandwritingView", "IsHandwritingViewEnabled", "KeepAliveCount", "AttachedView", "KeyboardAcceleratorTextOverride",
		"KeyboardAcceleratorTextOverride", "KeyboardAcceleratorTextOverride", "Background", "Cursor", "ElementSoundMode", "FocusState", "IsTabStop", "TabIndex", "XYFocusDown", "XYFocusDownNavigationStrategy",
		"XYFocusLeft", "XYFocusLeftNavigationStrategy", "XYFocusRight", "XYFocusRightNavigationStrategy", "XYFocusUp", "XYFocusUpNavigationStrategy", "AccessKey", "Description", "IconSource", "KeyboardAccelerators",
		"Label", "Foreground", "ShowAsMonochrome", "UriSource", "FontFamily", "FontSize", "FontStyle", "FontWeight", "Glyph", "IsTextScaleFactorEnabled",
		"MirroredWhenRightToLeft", "Data", "Symbol", "UseStrict", "CenterPoint", "Rotation", "Scale", "TransformMatrix", "Translation", "Shadow",
		"IconSource", "CommandingContainer", "CommandingTarget", "IsTelemetryCollectionEnabled", "IsTelemetryCollectionEnabled", "IsTelemetryCollectionEnabled", "CanPasteClipboardContent", "CanPasteClipboardContent", "CanRedo", "CanUndo",
		"ShowMode", "Target", "CornerRadius", null, null, null, null, null, "IsDialog", "DynamicOverflowOrder",
		"IsCompact", "IsInOverflow", "UseOverflowStyle", "BorderBrushProtected", "BorderThicknessProtected", "CornerRadiusProtected", "CanContentRenderOutsideBounds", "CanContentRenderOutsideBounds", "SelectionFlyout", "SelectionFlyout",
		"BackgroundSizing", "BackgroundSizing", "BackgroundSizing", "BackgroundSizing", "BackgroundSizing", "BackgroundSizing", null, null, null, "HorizontalAnchorRatio",
		"VerticalAnchorRatio", null, null, null, null, null, null, null, null, null,
		null, null, null, null, null, null, null, null, null, null,
		null, null, null, null, null, null, null, null, null, null,
		null, null, null, null, null, null, null, "Text", null, null,
		null, null, null, null, null, null, "Description", "PlacementRect", "SelectionFlyout", "SelectionFlyout",
		"SelectionFlyout", "BackgroundTransition", "BackgroundTransition", "BackgroundTransition", null, null, "Accent", "AltHigh", "AltLow", "AltMedium",
		"AltMediumHigh", "AltMediumLow", "BaseHigh", "BaseLow", "BaseMedium", "BaseMediumHigh", "BaseMediumLow", "ChromeAltLow", "ChromeBlackHigh", "ChromeBlackLow",
		"ChromeBlackMedium", "ChromeBlackMediumLow", "ChromeDisabledHigh", "ChromeDisabledLow", "ChromeGray", "ChromeHigh", "ChromeLow", "ChromeMedium", "ChromeMediumLow", "ChromeWhite",
		null, "ErrorText", "ListLow", "ListMedium", "TranslationTransition", "OpacityTransition", "RotationTransition", "ScaleTransition", null, null,
		"Duration", "Duration", "Duration", null, null, "Components", "IsOpen", null, null, null,
		null, null, null, null, "Kind", "CanBeScrollAnchor", null, null, "Receivers", "SizesContentToTemplatedParent",
		"TextBoxStyle", "IsNavigationStackEnabled", "ProofingMenuFlyout", "ProofingMenuFlyout", "HeaderPlacement", "HeaderPlacement", "HeaderPlacement", "HeaderPlacement", "HeaderPlacement", "HeaderPlacement",
		"HeaderPlacement", "HeaderPlacement", "HeaderPlacement", "HeaderPlacement", "ReduceViewportForCoreInputViewOcclusions", "AreOpenCloseAnimationsEnabled", "InputDevicePrefersPrimaryCommands", "InputValidationKind", "InputValidationMode", "Description",
		null, null, null, null, null, null, null, "Description", "ErrorTemplate", "HasValidationErrors",
		"InputValidationKind", "InputValidationMode", "ValidationCommand", "ValidationContext", "ValidationErrors", "Description", null, null, null, null,
		null, null, null, "ErrorTemplate", "HasValidationErrors", "InputValidationKind", "InputValidationMode", "ValidationCommand", "ValidationContext", "ValidationErrors",
		"Description", "ErrorTemplate", "HasValidationErrors", "InputValidationKind", "InputValidationMode", "ValidationCommand", "ValidationContext", "ValidationErrors", "Description", "ErrorTemplate",
		"HasValidationErrors", "InputValidationKind", "InputValidationMode", "ValidationCommand", "ValidationContext", "ValidationErrors", "AccessKey", "Command", "Description", "IconSource",
		"KeyboardAccelerators", "Label", "ThemeShadowReceiverCount", "OverlayInputPassThroughElement", "SelectedDate", "SelectedTime", null, null, "ColorCloseThreshold", "HostElement",
		"PreventAutomaticDismiss", "ShouldInjectEnterKeyPress", "ShowEmojiButtonInPlaceOfSwitchToKeyboard", "ShowEnterButtonForSingleLineTextBox", null, "PreventDismissOnPointer", "NegativeCompactVerticalDelta", "NegativeHiddenVerticalDelta", "NegativeMinimalVerticalDelta", null,
		null, null, null, null, null, null, null, "ShouldConstrainToRootBounds", "ShouldConstrainToRootBounds", "IsDefaultShadowEnabled",
		"IsDefaultShadowEnabled", "ActualOffset", "ActualSize", "OverflowContentCompactYTranslation", "OverflowContentHiddenYTranslation", "OverflowContentMinimalYTranslation", "BaseUri", "RootObject", "TargetObject", "TargetProperty",
		"ShouldConstrainToRootBounds"
	};

	private static readonly Dictionary<int, string>[] enumValues = new Dictionary<int, string>[395]
	{
		new Dictionary<int, string>
		{
			{ 0, "Raw" },
			{ 1, "Control" },
			{ 2, "Content" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Left" },
			{ 1, "Center" },
			{ 2, "Right" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Top" },
			{ 1, "Center" },
			{ 2, "Bottom" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Left" },
			{ 1, "Top" },
			{ 2, "Right" },
			{ 3, "Bottom" }
		},
		new Dictionary<int, string>
		{
			{ 60000, "Unknown" },
			{ 60001, "SpellingError" },
			{ 60002, "GrammarError" },
			{ 60003, "Comment" },
			{ 60004, "FormulaError" },
			{ 60005, "TrackChanges" },
			{ 60006, "Header" },
			{ 60007, "Footer" },
			{ 60008, "Highlighted" },
			{ 60009, "Endnote" },
			{ 60010, "Footnote" },
			{ 60011, "InsertionChange" },
			{ 60012, "DeletionChange" },
			{ 60013, "MoveChange" },
			{ 60014, "FormatChange" },
			{ 60015, "UnsyncedChange" },
			{ 60016, "EditingLockedChange" },
			{ 60017, "ExternalChange" },
			{ 60018, "ConflictingChange" },
			{ 60019, "Author" },
			{ 60020, "AdvancedProofingIssue" },
			{ 60021, "DataValidationError" },
			{ 60022, "CircularReferenceError" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Compact" },
			{ 1, "Minimal" },
			{ 2, "Hidden" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Light" },
			{ 1, "Dark" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Other" },
			{ 1, "ForegroundOnlyMedia" },
			{ 2, "BackgroundCapableMedia" },
			{ 3, "Communications" },
			{ 4, "Alerts" },
			{ 5, "SoundEffects" },
			{ 6, "GameEffects" },
			{ 7, "GameMedia" },
			{ 8, "GameChat" },
			{ 9, "Speech" },
			{ 10, "Movie" },
			{ 11, "Media" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Console" },
			{ 1, "Multimedia" },
			{ 2, "Communications" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Button" },
			{ 1, "Calendar" },
			{ 2, "CheckBox" },
			{ 3, "ComboBox" },
			{ 4, "Edit" },
			{ 5, "Hyperlink" },
			{ 6, "Image" },
			{ 7, "ListItem" },
			{ 8, "List" },
			{ 9, "Menu" },
			{ 10, "MenuBar" },
			{ 11, "MenuItem" },
			{ 12, "ProgressBar" },
			{ 13, "RadioButton" },
			{ 14, "ScrollBar" },
			{ 15, "Slider" },
			{ 16, "Spinner" },
			{ 17, "StatusBar" },
			{ 18, "Tab" },
			{ 19, "TabItem" },
			{ 20, "Text" },
			{ 21, "ToolBar" },
			{ 22, "ToolTip" },
			{ 23, "Tree" },
			{ 24, "TreeItem" },
			{ 25, "Custom" },
			{ 26, "Group" },
			{ 27, "Thumb" },
			{ 28, "DataGrid" },
			{ 29, "DataItem" },
			{ 30, "Document" },
			{ 31, "SplitButton" },
			{ 32, "Window" },
			{ 33, "Pane" },
			{ 34, "Header" },
			{ 35, "HeaderItem" },
			{ 36, "Table" },
			{ 37, "TitleBar" },
			{ 38, "Separator" },
			{ 39, "SemanticZoom" },
			{ 40, "AppBar" }
		},
		new Dictionary<int, string>
		{
			{ 0, "ToolTipOpened" },
			{ 1, "ToolTipClosed" },
			{ 2, "MenuOpened" },
			{ 3, "MenuClosed" },
			{ 4, "AutomationFocusChanged" },
			{ 5, "InvokePatternOnInvoked" },
			{ 6, "SelectionItemPatternOnElementAddedToSelection" },
			{ 7, "SelectionItemPatternOnElementRemovedFromSelection" },
			{ 8, "SelectionItemPatternOnElementSelected" },
			{ 9, "SelectionPatternOnInvalidated" },
			{ 10, "TextPatternOnTextSelectionChanged" },
			{ 11, "TextPatternOnTextChanged" },
			{ 12, "AsyncContentLoaded" },
			{ 13, "PropertyChanged" },
			{ 14, "StructureChanged" },
			{ 15, "DragStart" },
			{ 16, "DragCancel" },
			{ 17, "DragComplete" },
			{ 18, "DragEnter" },
			{ 19, "DragLeave" },
			{ 20, "Dropped" },
			{ 21, "LiveRegionChanged" },
			{ 22, "InputReachedTarget" },
			{ 23, "InputReachedOtherElement" },
			{ 24, "InputDiscarded" },
			{ 25, "WindowClosed" },
			{ 26, "WindowOpened" },
			{ 27, "ConversionTargetChanged" },
			{ 28, "TextEditTextChanged" },
			{ 29, "LayoutInvalidated" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Off" },
			{ 1, "Polite" },
			{ 2, "Assertive" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Horizontal" },
			{ 2, "Vertical" }
		},
		new Dictionary<int, string>
		{
			{ 0, "UserInput" },
			{ 1, "ProgrammaticChange" },
			{ 2, "SuggestionChosen" }
		},
		new Dictionary<int, string>
		{
			{ 1, "OneWay" },
			{ 2, "OneTime" },
			{ 3, "TwoWay" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 8, "IgnoreImageCache" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Absolute" },
			{ 1, "RelativeToBoundingBox" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Release" },
			{ 1, "Press" },
			{ 2, "Hover" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Active" },
			{ 1, "Filling" },
			{ 2, "Stopped" },
			{ 3, "NotStarted" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "Reset" },
			{ 1, "ItemInserted" },
			{ 2, "ItemRemoved" },
			{ 3, "ItemChanged" }
		},
		new Dictionary<int, string>
		{
			{ 0, "ScRgbLinearInterpolation" },
			{ 1, "SRgbLinearInterpolation" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Application" },
			{ 1, "Nested" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Primary" },
			{ 2, "Secondary" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Physical" },
			{ 1, "Logical" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Top" },
			{ 1, "Left" },
			{ 2, "Bottom" },
			{ 3, "Right" },
			{ 4, "Fill" },
			{ 5, "None" }
		},
		new Dictionary<int, string>
		{
			{ 0, "EaseOut" },
			{ 1, "EaseIn" },
			{ 2, "EaseInOut" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Left" },
			{ 1, "Top" },
			{ 2, "Right" },
			{ 3, "Bottom" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Inherit" },
			{ 1, "SourceOver" },
			{ 2, "MinBlend" },
			{ 3, "DestInvert" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Default" },
			{ 1, "Light" },
			{ 2, "Dark" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Collapsed" },
			{ 1, "Expanded" },
			{ 2, "PartiallyExpanded" },
			{ 3, "LeafNode" }
		},
		new Dictionary<int, string>
		{
			{ 0, "HoldEnd" },
			{ 1, "Stop" }
		},
		new Dictionary<int, string>
		{
			{ 0, "EvenOdd" },
			{ 1, "Nonzero" }
		},
		new Dictionary<int, string>
		{
			{ 0, "LeftToRight" },
			{ 1, "RightToLeft" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Top" },
			{ 1, "Bottom" },
			{ 2, "Left" },
			{ 3, "Right" },
			{ 4, "Full" },
			{ 5, "TopEdgeAlignedLeft" },
			{ 6, "TopEdgeAlignedRight" },
			{ 7, "BottomEdgeAlignedLeft" },
			{ 8, "BottomEdgeAlignedRight" },
			{ 9, "LeftEdgeAlignedTop" },
			{ 10, "LeftEdgeAlignedBottom" },
			{ 11, "RightEdgeAlignedTop" },
			{ 12, "RightEdgeAlignedBottom" },
			{ 13, "Auto" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Next" },
			{ 1, "Previous" },
			{ 2, "Up" },
			{ 3, "Down" },
			{ 4, "Left" },
			{ 5, "Right" },
			{ 6, "None" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Unfocused" },
			{ 1, "Pointer" },
			{ 2, "Keyboard" },
			{ 3, "Programmatic" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Normal" },
			{ 1, "AllSmallCaps" },
			{ 2, "SmallCaps" },
			{ 3, "AllPetiteCaps" },
			{ 4, "PetiteCaps" },
			{ 5, "Unicase" },
			{ 6, "Titling" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Normal" },
			{ 1, "HojoKanji" },
			{ 2, "Jis04" },
			{ 3, "Jis78" },
			{ 4, "Jis83" },
			{ 5, "Jis90" },
			{ 6, "NlcKanji" },
			{ 7, "Simplified" },
			{ 8, "Traditional" },
			{ 9, "TraditionalNames" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Normal" },
			{ 1, "Full" },
			{ 2, "Half" },
			{ 3, "Proportional" },
			{ 4, "Quarter" },
			{ 5, "Third" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Normal" },
			{ 1, "Stacked" },
			{ 2, "Slashed" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Normal" },
			{ 1, "Proportional" },
			{ 2, "Tabular" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Normal" },
			{ 1, "Lining" },
			{ 2, "OldStyle" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Undefined" },
			{ 1, "UltraCondensed" },
			{ 2, "ExtraCondensed" },
			{ 3, "Condensed" },
			{ 4, "SemiCondensed" },
			{ 5, "Normal" },
			{ 6, "SemiExpanded" },
			{ 7, "Expanded" },
			{ 8, "ExtraExpanded" },
			{ 9, "UltraExpanded" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Normal" },
			{ 1, "Oblique" },
			{ 2, "Italic" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Normal" },
			{ 1, "Superscript" },
			{ 2, "Subscript" },
			{ 3, "Ordinal" },
			{ 4, "Inferior" },
			{ 5, "Ruby" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Forward" },
			{ 1, "Backward" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Tapped" },
			{ 2, "DoubleTapped" },
			{ 3, "RightTapped" },
			{ 4, "Holding" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Pad" },
			{ 1, "Reflect" },
			{ 2, "Repeat" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Pixel" },
			{ 2, "Star" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Top" },
			{ 1, "Left" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Started" },
			{ 1, "Completed" },
			{ 2, "Canceled" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Left" },
			{ 1, "Center" },
			{ 2, "Right" },
			{ 3, "Stretch" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Edge" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Default" },
			{ 1, "Url" },
			{ 5, "EmailSmtpAddress" },
			{ 7, "PersonalFullName" },
			{ 20, "CurrencyAmountAndSymbol" },
			{ 21, "CurrencyAmount" },
			{ 23, "DateMonthNumber" },
			{ 24, "DateDayNumber" },
			{ 25, "DateYear" },
			{ 28, "Digits" },
			{ 29, "Number" },
			{ 31, "Password" },
			{ 32, "TelephoneNumber" },
			{ 33, "TelephoneCountryCode" },
			{ 34, "TelephoneAreaCode" },
			{ 35, "TelephoneLocalNumber" },
			{ 37, "TimeHour" },
			{ 38, "TimeMinutesOrSeconds" },
			{ 39, "NumberFullWidth" },
			{ 40, "AlphanumericHalfWidth" },
			{ 41, "AlphanumericFullWidth" },
			{ 44, "Hiragana" },
			{ 45, "KatakanaHalfWidth" },
			{ 46, "KatakanaFullWidth" },
			{ 47, "Hanja" },
			{ 48, "HangulHalfWidth" },
			{ 49, "HangulFullWidth" },
			{ 50, "Search" },
			{ 51, "Formula" },
			{ 52, "SearchIncremental" },
			{ 53, "ChineseHalfWidth" },
			{ 54, "ChineseFullWidth" },
			{ 55, "NativeScript" },
			{ 57, "Text" },
			{ 58, "Chat" },
			{ 59, "NameOrPhoneNumber" },
			{ 60, "EmailNameOrAddress" },
			{ 62, "Maps" },
			{ 63, "NumericPassword" },
			{ 64, "NumericPin" },
			{ 65, "AlphanumericPin" },
			{ 67, "FormulaNumber" },
			{ 68, "ChatWithoutEmoji" }
		},
		new Dictionary<int, string>
		{
			{ 0, "KeepItemsInView" },
			{ 1, "KeepScrollOffset" },
			{ 2, "KeepLastItemInView" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Local" },
			{ 1, "Cycle" },
			{ 2, "Once" }
		},
		new Dictionary<int, string>
		{
			{ 0, "MaxHeight" },
			{ 1, "BlockLineHeight" },
			{ 2, "BaselineToBaseline" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Disabled" },
			{ 1, "Enabled" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Single" },
			{ 2, "Multiple" },
			{ 3, "Extended" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Backward" },
			{ 1, "Forward" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "TranslateX" },
			{ 2, "TranslateY" },
			{ 4, "TranslateRailsX" },
			{ 8, "TranslateRailsY" },
			{ 16, "Rotate" },
			{ 32, "Scale" },
			{ 64, "TranslateInertia" },
			{ 128, "RotateInertia" },
			{ 256, "ScaleInertia" },
			{ 65535, "All" },
			{ 65536, "System" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Extension" },
			{ 2, "Binding" }
		},
		new Dictionary<int, string>
		{
			{ 0, "NotSupported" },
			{ 1, "Maybe" },
			{ 2, "Probably" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Closed" },
			{ 1, "Opening" },
			{ 2, "Buffering" },
			{ 3, "Playing" },
			{ 4, "Paused" },
			{ 5, "Stopped" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Disabled" },
			{ 1, "Required" },
			{ 2, "Enabled" }
		},
		new Dictionary<int, string>
		{
			{ 0, "New" },
			{ 1, "Back" },
			{ 2, "Forward" },
			{ 3, "Refresh" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Add" },
			{ 1, "Remove" },
			{ 2, "Replace" },
			{ 3, "Move" },
			{ 4, "Reset" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "TrimSideBearings" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Vertical" },
			{ 1, "Horizontal" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Forward" },
			{ 2, "Backward" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Invoke" },
			{ 1, "Selection" },
			{ 2, "Value" },
			{ 3, "RangeValue" },
			{ 4, "Scroll" },
			{ 5, "ScrollItem" },
			{ 6, "ExpandCollapse" },
			{ 7, "Grid" },
			{ 8, "GridItem" },
			{ 9, "MultipleView" },
			{ 10, "Window" },
			{ 11, "SelectionItem" },
			{ 12, "Dock" },
			{ 13, "Table" },
			{ 14, "TableItem" },
			{ 15, "Toggle" },
			{ 16, "Transform" },
			{ 17, "Text" },
			{ 18, "ItemContainer" },
			{ 19, "VirtualizedItem" },
			{ 20, "Text2" },
			{ 21, "TextChild" },
			{ 22, "TextRange" },
			{ 23, "Annotation" },
			{ 24, "Drag" },
			{ 25, "DropTarget" },
			{ 26, "ObjectModel" },
			{ 27, "Spreadsheet" },
			{ 28, "SpreadsheetItem" },
			{ 29, "Styles" },
			{ 30, "Transform2" },
			{ 31, "SynchronizedInput" },
			{ 32, "TextEdit" },
			{ 33, "CustomNavigation" },
			{ 34, "SeeitSayit" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Flat" },
			{ 1, "Square" },
			{ 2, "Round" },
			{ 3, "Triangle" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Miter" },
			{ 1, "Bevel" },
			{ 2, "Round" }
		},
		new Dictionary<int, string>
		{
			{ 2, "Bottom" },
			{ 4, "Right" },
			{ 7, "Mouse" },
			{ 9, "Left" },
			{ 10, "Top" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Touch" },
			{ 1, "Pen" },
			{ 2, "Mouse" }
		},
		new Dictionary<int, string>
		{
			{ 0, "PointerDirection_XAxis" },
			{ 1, "PointerDirection_YAxis" },
			{ 2, "PointerDirection_BothAxes" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Final" },
			{ 1, "Intermediate" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Bitmap" },
			{ 1, "Vector" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "TemplatedParent" },
			{ 2, "Self" }
		},
		new Dictionary<int, string>
		{
			{ 0, "RowMajor" },
			{ 1, "ColumnMajor" },
			{ 2, "Indeterminate" }
		},
		new Dictionary<int, string>
		{
			{ 0, "LargeDecrement" },
			{ 1, "SmallDecrement" },
			{ 2, "NoAmount" },
			{ 3, "LargeIncrement" },
			{ 4, "SmallIncrement" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Disabled" },
			{ 1, "Auto" },
			{ 2, "Hidden" },
			{ 3, "Visible" }
		},
		new Dictionary<int, string>
		{
			{ 0, "SmallDecrement" },
			{ 1, "SmallIncrement" },
			{ 2, "LargeDecrement" },
			{ 3, "LargeIncrement" },
			{ 4, "ThumbPosition" },
			{ 5, "ThumbTrack" },
			{ 6, "First" },
			{ 7, "Last" },
			{ 8, "EndScroll" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "TouchIndicator" },
			{ 2, "MouseIndicator" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Default" },
			{ 1, "Leading" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Disabled" },
			{ 1, "Enabled" },
			{ 2, "Auto" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Single" },
			{ 1, "Multiple" },
			{ 2, "Extended" }
		},
		new Dictionary<int, string>
		{
			{ 0, "StepValues" },
			{ 1, "Ticks" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Near" },
			{ 1, "Center" },
			{ 2, "Far" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Optional" },
			{ 2, "Mandatory" },
			{ 3, "OptionalSingle" },
			{ 4, "MandatorySingle" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "SideBySide" },
			{ 2, "TopBottom" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Mono" },
			{ 1, "Stereo" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Fill" },
			{ 2, "Uniform" },
			{ 3, "UniformToFill" }
		},
		new Dictionary<int, string>
		{
			{ 0, "UpOnly" },
			{ 1, "DownOnly" },
			{ 2, "Both" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "BoldSimulation" },
			{ 2, "ItalicSimulation" },
			{ 3, "BoldItalicSimulation" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Single" },
			{ 2, "Multiple" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Counterclockwise" },
			{ 1, "Clockwise" }
		},
		new Dictionary<int, string>
		{
			{ 57600, "Previous" },
			{ 57601, "Next" },
			{ 57602, "Play" },
			{ 57603, "Pause" },
			{ 57604, "Edit" },
			{ 57605, "Save" },
			{ 57606, "Clear" },
			{ 57607, "Delete" },
			{ 57608, "Remove" },
			{ 57609, "Add" },
			{ 57610, "Cancel" },
			{ 57611, "Accept" },
			{ 57612, "More" },
			{ 57613, "Redo" },
			{ 57614, "Undo" },
			{ 57615, "Home" },
			{ 57616, "Up" },
			{ 57617, "Forward" },
			{ 57618, "Back" },
			{ 57619, "Favorite" },
			{ 57620, "Camera" },
			{ 57621, "Setting" },
			{ 57622, "Video" },
			{ 57623, "Sync" },
			{ 57624, "Download" },
			{ 57625, "Mail" },
			{ 57626, "Find" },
			{ 57627, "Help" },
			{ 57628, "Upload" },
			{ 57629, "Emoji" },
			{ 57630, "TwoPage" },
			{ 57631, "LeaveChat" },
			{ 57632, "MailForward" },
			{ 57633, "Clock" },
			{ 57634, "Send" },
			{ 57635, "Crop" },
			{ 57636, "RotateCamera" },
			{ 57637, "People" },
			{ 57638, "OpenPane" },
			{ 57639, "ClosePane" },
			{ 57640, "World" },
			{ 57641, "Flag" },
			{ 57642, "PreviewLink" },
			{ 57643, "Globe" },
			{ 57644, "Trim" },
			{ 57645, "AttachCamera" },
			{ 57646, "ZoomIn" },
			{ 57647, "Bookmarks" },
			{ 57648, "Document" },
			{ 57649, "ProtectedDocument" },
			{ 57650, "Page" },
			{ 57651, "Bullets" },
			{ 57652, "Comment" },
			{ 57653, "MailFilled" },
			{ 57654, "ContactInfo" },
			{ 57655, "HangUp" },
			{ 57656, "ViewAll" },
			{ 57657, "MapPin" },
			{ 57658, "Phone" },
			{ 57659, "VideoChat" },
			{ 57660, "Switch" },
			{ 57661, "Contact" },
			{ 57662, "Rename" },
			{ 57665, "Pin" },
			{ 57666, "MusicInfo" },
			{ 57667, "Go" },
			{ 57668, "Keyboard" },
			{ 57669, "DockLeft" },
			{ 57670, "DockRight" },
			{ 57671, "DockBottom" },
			{ 57672, "Remote" },
			{ 57673, "Refresh" },
			{ 57674, "Rotate" },
			{ 57675, "Shuffle" },
			{ 57676, "List" },
			{ 57677, "Shop" },
			{ 57678, "SelectAll" },
			{ 57679, "Orientation" },
			{ 57680, "Import" },
			{ 57681, "ImportAll" },
			{ 57685, "BrowsePhotos" },
			{ 57686, "WebCam" },
			{ 57688, "Pictures" },
			{ 57689, "SaveLocal" },
			{ 57690, "Caption" },
			{ 57691, "Stop" },
			{ 57692, "ShowResults" },
			{ 57693, "Volume" },
			{ 57694, "Repair" },
			{ 57695, "Message" },
			{ 57696, "Page2" },
			{ 57697, "CalendarDay" },
			{ 57698, "CalendarWeek" },
			{ 57699, "Calendar" },
			{ 57700, "Character" },
			{ 57701, "MailReplyAll" },
			{ 57702, "Read" },
			{ 57703, "Link" },
			{ 57704, "Account" },
			{ 57705, "ShowBcc" },
			{ 57706, "HideBcc" },
			{ 57707, "Cut" },
			{ 57708, "Attach" },
			{ 57709, "Paste" },
			{ 57710, "Filter" },
			{ 57711, "Copy" },
			{ 57712, "Emoji2" },
			{ 57713, "Important" },
			{ 57714, "MailReply" },
			{ 57715, "SlideShow" },
			{ 57716, "Sort" },
			{ 57720, "Manage" },
			{ 57721, "AllApps" },
			{ 57722, "DisconnectDrive" },
			{ 57723, "MapDrive" },
			{ 57724, "NewWindow" },
			{ 57725, "OpenWith" },
			{ 57729, "ContactPresence" },
			{ 57730, "Priority" },
			{ 57732, "GoToToday" },
			{ 57733, "Font" },
			{ 57734, "FontColor" },
			{ 57735, "Contact2" },
			{ 57736, "Folder" },
			{ 57737, "Audio" },
			{ 57738, "Placeholder" },
			{ 57739, "View" },
			{ 57740, "SetLockScreen" },
			{ 57741, "SetTile" },
			{ 57744, "ClosedCaption" },
			{ 57745, "StopSlideShow" },
			{ 57746, "Permissions" },
			{ 57747, "Highlight" },
			{ 57748, "DisableUpdates" },
			{ 57749, "UnFavorite" },
			{ 57750, "UnPin" },
			{ 57751, "OpenLocal" },
			{ 57752, "Mute" },
			{ 57753, "Italic" },
			{ 57754, "Underline" },
			{ 57755, "Bold" },
			{ 57756, "MoveToFolder" },
			{ 57757, "LikeDislike" },
			{ 57758, "Dislike" },
			{ 57759, "Like" },
			{ 57760, "AlignRight" },
			{ 57761, "AlignCenter" },
			{ 57762, "AlignLeft" },
			{ 57763, "Zoom" },
			{ 57764, "ZoomOut" },
			{ 57765, "OpenFile" },
			{ 57766, "OtherUser" },
			{ 57767, "Admin" },
			{ 57795, "Street" },
			{ 57796, "Map" },
			{ 57797, "ClearSelection" },
			{ 57798, "FontDecrease" },
			{ 57799, "FontIncrease" },
			{ 57800, "FontSize" },
			{ 57801, "CellPhone" },
			{ 57802, "ReShare" },
			{ 57803, "Tag" },
			{ 57804, "RepeatOne" },
			{ 57805, "RepeatAll" },
			{ 57806, "OutlineStar" },
			{ 57807, "SolidStar" },
			{ 57808, "Calculator" },
			{ 57809, "Directions" },
			{ 57810, "Target" },
			{ 57811, "Library" },
			{ 57812, "PhoneBook" },
			{ 57813, "Memo" },
			{ 57814, "Microphone" },
			{ 57815, "PostUpdate" },
			{ 57816, "BackToWindow" },
			{ 57817, "FullScreen" },
			{ 57818, "NewFolder" },
			{ 57819, "CalendarReply" },
			{ 57821, "UnSyncFolder" },
			{ 57822, "ReportHacked" },
			{ 57823, "SyncFolder" },
			{ 57824, "BlockContact" },
			{ 57825, "SwitchApps" },
			{ 57826, "AddFriend" },
			{ 57827, "TouchPointer" },
			{ 57828, "GoToStart" },
			{ 57829, "ZeroBars" },
			{ 57830, "OneBar" },
			{ 57831, "TwoBars" },
			{ 57832, "ThreeBars" },
			{ 57833, "FourBars" },
			{ 58004, "Scan" },
			{ 58005, "Preview" },
			{ 59136, "GlobalNavigationButton" },
			{ 59181, "Share" },
			{ 59209, "Print" },
			{ 59792, "XboxOneConsole" }
		},
		new Dictionary<int, string>
		{
			{ 1, "KeyUp" },
			{ 2, "KeyDown" },
			{ 4, "LeftMouseUp" },
			{ 8, "LeftMouseDown" },
			{ 16, "RightMouseUp" },
			{ 32, "RightMouseDown" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Center" },
			{ 1, "Left" },
			{ 2, "Right" },
			{ 3, "Justify" },
			{ 4, "DetectFromContent" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Ideal" },
			{ 1, "Display" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Fixed" },
			{ 1, "Animated" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Full" },
			{ 1, "TrimToCapHeight" },
			{ 2, "TrimToBaseline" },
			{ 3, "Tight" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Default" },
			{ 1, "DetectFromContent" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Aliased" },
			{ 2, "Grayscale" },
			{ 3, "ClearType" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "CharacterEllipsis" },
			{ 2, "WordEllipsis" },
			{ 3, "Clip" }
		},
		new Dictionary<int, string>
		{
			{ 1, "NoWrap" },
			{ 2, "Wrap" },
			{ 3, "WrapWholeWords" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "TopLeft" },
			{ 2, "BottomRight" },
			{ 3, "Outside" },
			{ 4, "Inline" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Off" },
			{ 1, "On" },
			{ 2, "Indeterminate" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Primitive" },
			{ 1, "Metadata" },
			{ 2, "Custom" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Default" },
			{ 1, "PropertyChanged" },
			{ 2, "Explicit" },
			{ 3, "LostFocus" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Top" },
			{ 1, "Center" },
			{ 2, "Bottom" },
			{ 3, "Stretch" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Standard" },
			{ 1, "Recycling" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "LeftButton" },
			{ 2, "RightButton" },
			{ 3, "Cancel" },
			{ 4, "MiddleButton" },
			{ 5, "XButton1" },
			{ 6, "XButton2" },
			{ 8, "Back" },
			{ 9, "Tab" },
			{ 12, "Clear" },
			{ 13, "Enter" },
			{ 16, "Shift" },
			{ 17, "Control" },
			{ 18, "Menu" },
			{ 19, "Pause" },
			{ 20, "CapitalLock" },
			{ 21, "Kana" },
			{ 23, "Junja" },
			{ 24, "Final" },
			{ 25, "Kanji" },
			{ 27, "Escape" },
			{ 28, "Convert" },
			{ 29, "NonConvert" },
			{ 30, "Accept" },
			{ 31, "ModeChange" },
			{ 32, "Space" },
			{ 33, "PageUp" },
			{ 34, "PageDown" },
			{ 35, "End" },
			{ 36, "Home" },
			{ 37, "Left" },
			{ 38, "Up" },
			{ 39, "Right" },
			{ 40, "Down" },
			{ 41, "Select" },
			{ 42, "Print" },
			{ 43, "Execute" },
			{ 44, "Snapshot" },
			{ 45, "Insert" },
			{ 46, "Delete" },
			{ 47, "Help" },
			{ 48, "Number0" },
			{ 49, "Number1" },
			{ 50, "Number2" },
			{ 51, "Number3" },
			{ 52, "Number4" },
			{ 53, "Number5" },
			{ 54, "Number6" },
			{ 55, "Number7" },
			{ 56, "Number8" },
			{ 57, "Number9" },
			{ 65, "A" },
			{ 66, "B" },
			{ 67, "C" },
			{ 68, "D" },
			{ 69, "E" },
			{ 70, "F" },
			{ 71, "G" },
			{ 72, "H" },
			{ 73, "I" },
			{ 74, "J" },
			{ 75, "K" },
			{ 76, "L" },
			{ 77, "M" },
			{ 78, "N" },
			{ 79, "O" },
			{ 80, "P" },
			{ 81, "Q" },
			{ 82, "R" },
			{ 83, "S" },
			{ 84, "T" },
			{ 85, "U" },
			{ 86, "V" },
			{ 87, "W" },
			{ 88, "X" },
			{ 89, "Y" },
			{ 90, "Z" },
			{ 91, "LeftWindows" },
			{ 92, "RightWindows" },
			{ 93, "Application" },
			{ 95, "Sleep" },
			{ 96, "NumberPad0" },
			{ 97, "NumberPad1" },
			{ 98, "NumberPad2" },
			{ 99, "NumberPad3" },
			{ 100, "NumberPad4" },
			{ 101, "NumberPad5" },
			{ 102, "NumberPad6" },
			{ 103, "NumberPad7" },
			{ 104, "NumberPad8" },
			{ 105, "NumberPad9" },
			{ 106, "Multiply" },
			{ 107, "Add" },
			{ 108, "Separator" },
			{ 109, "Subtract" },
			{ 110, "Decimal" },
			{ 111, "Divide" },
			{ 112, "F1" },
			{ 113, "F2" },
			{ 114, "F3" },
			{ 115, "F4" },
			{ 116, "F5" },
			{ 117, "F6" },
			{ 118, "F7" },
			{ 119, "F8" },
			{ 120, "F9" },
			{ 121, "F10" },
			{ 122, "F11" },
			{ 123, "F12" },
			{ 124, "F13" },
			{ 125, "F14" },
			{ 126, "F15" },
			{ 127, "F16" },
			{ 128, "F17" },
			{ 129, "F18" },
			{ 130, "F19" },
			{ 131, "F20" },
			{ 132, "F21" },
			{ 133, "F22" },
			{ 134, "F23" },
			{ 135, "F24" },
			{ 144, "NumberKeyLock" },
			{ 145, "Scroll" },
			{ 160, "LeftShift" },
			{ 161, "RightShift" },
			{ 162, "LeftControl" },
			{ 163, "RightControl" },
			{ 164, "LeftMenu" },
			{ 165, "RightMenu" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Control" },
			{ 2, "Menu" },
			{ 4, "Shift" },
			{ 8, "Windows" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Visible" },
			{ 1, "Collapsed" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Running" },
			{ 1, "Closing" },
			{ 2, "ReadyForUserInteraction" },
			{ 3, "BlockedByModalWindow" },
			{ 4, "NotResponding" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Normal" },
			{ 1, "Maximized" },
			{ 2, "Minimized" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Disabled" },
			{ 1, "Enabled" }
		},
		new Dictionary<int, string>
		{
			{ 0, "NoAmount" },
			{ 1, "LargeDecrement" },
			{ 2, "SmallDecrement" },
			{ 3, "LargeIncrement" },
			{ 4, "SmallIncrement" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Parent" },
			{ 1, "NextSibling" },
			{ 2, "PreviousSibling" },
			{ 3, "FirstChild" },
			{ 4, "LastChild" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Month" },
			{ 1, "Year" },
			{ 2, "Decade" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Single" },
			{ 2, "Multiple" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Sunday" },
			{ 1, "Monday" },
			{ 2, "Tuesday" },
			{ 3, "Wednesday" },
			{ 4, "Thursday" },
			{ 5, "Friday" },
			{ 6, "Saturday" }
		},
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Copy" },
			{ 2, "Move" },
			{ 4, "Link" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Overlay" },
			{ 1, "Inline" },
			{ 2, "CompactOverlay" },
			{ 3, "CompactInline" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Left" },
			{ 1, "Right" }
		},
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Start" },
			{ 2, "End" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "LasVegasLights" },
			{ 2, "BlinkingBackground" },
			{ 3, "SparkleText" },
			{ 4, "MarchingBlackAnts" },
			{ 5, "MarchingRedAnts" },
			{ 6, "Shimmer" },
			{ 7, "Other" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "HollowRoundBullet" },
			{ 2, "FilledRoundBullet" },
			{ 3, "HollowSquareBullet" },
			{ 4, "FilledSquareBullet" },
			{ 5, "DashBullet" },
			{ 6, "Other" }
		},
		new Dictionary<int, string>
		{
			{ 0, "LTR" },
			{ 1, "RTL" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Unknown" },
			{ 1, "EndOfLine" },
			{ 2, "BeginningOfLine" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Default" },
			{ 1, "RightToLeft" },
			{ 2, "BottomToTop" },
			{ 3, "Vertical" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Outline" },
			{ 2, "Shadow" },
			{ 3, "Engraved" },
			{ 4, "Embossed" }
		},
		new Dictionary<int, string>
		{
			{ 70001, "Heading1" },
			{ 70002, "Heading2" },
			{ 70003, "Heading3" },
			{ 70004, "Heading4" },
			{ 70005, "Heading5" },
			{ 70006, "Heading6" },
			{ 70007, "Heading7" },
			{ 70008, "Heading8" },
			{ 70009, "Heading9" },
			{ 70010, "Title" },
			{ 70011, "Subtitle" },
			{ 70012, "Normal" },
			{ 70013, "Emphasis" },
			{ 70014, "Quote" },
			{ 70015, "BulletedList" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Single" },
			{ 2, "WordsOnly" },
			{ 3, "Double" },
			{ 4, "Dot" },
			{ 5, "Dash" },
			{ 6, "DashDot" },
			{ 7, "DashDotDot" },
			{ 8, "Wavy" },
			{ 9, "ThickSingle" },
			{ 10, "DoubleWavy" },
			{ 11, "ThickWavy" },
			{ 12, "LongDash" },
			{ 13, "ThickDash" },
			{ 14, "ThickDashDot" },
			{ 15, "ThickDashDotDot" },
			{ 16, "ThickDot" },
			{ 17, "ThickLongDash" },
			{ 18, "Other" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "AutoCorrect" },
			{ 2, "Composition" },
			{ 3, "CompositionFinalized" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "ChildAdded" },
			{ 1, "ChildRemoved" },
			{ 2, "ChildrenInvalidated" },
			{ 3, "ChildrenBulkAdded" },
			{ 4, "ChildrenBulkRemoved" },
			{ 5, "ChildrenReordered" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Peek" },
			{ 1, "Hidden" },
			{ 2, "Visible" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "Unknown" },
			{ 1, "Audio" },
			{ 2, "Video" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Inline" },
			{ 1, "Overlay" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "SameThread" },
			{ 1, "SeparateThread" },
			{ 2, "SeparateProcess" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "Unknown" },
			{ 1, "Defer" },
			{ 2, "Allow" },
			{ 3, "Deny" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Geolocation" },
			{ 1, "UnlimitedIndexedDBQuota" },
			{ 2, "Media" },
			{ 3, "PointerLock" },
			{ 4, "WebNotifications" },
			{ 5, "Screen" },
			{ 6, "ImmersiveView" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "Default" },
			{ 1, "BottomEdge" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Single" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "AllFormats" },
			{ 1, "PlainText" }
		},
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Custom" },
			{ 2, "Form" },
			{ 3, "Main" },
			{ 4, "Navigation" },
			{ 5, "Search" }
		},
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Default" },
			{ 1, "Collapsed" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "Bottom" },
			{ 1, "Right" },
			{ 2, "Collapsed" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Visible" },
			{ 2, "Collapsed" }
		},
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "AddingToOverflow" },
			{ 1, "RemovingFromOverflow" }
		},
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "On" },
			{ 2, "Off" }
		},
		new Dictionary<int, string>
		{
			{ 0, "DottedLine" },
			{ 1, "HighVisibility" },
			{ 2, "Reveal" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Never" },
			{ 1, "WhenEngaged" },
			{ 2, "WhenFocused" }
		},
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Skip" },
			{ 1, "Hide" },
			{ 2, "Disable" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Focus" },
			{ 1, "Invoke" },
			{ 2, "Show" },
			{ 3, "Hide" },
			{ 4, "MovePrevious" },
			{ 5, "MoveNext" },
			{ 6, "GoBack" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Default" },
			{ 1, "FocusOnly" },
			{ 2, "Off" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Off" },
			{ 2, "On" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "WhenRequested" }
		},
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Underline" },
			{ 2, "Strikethrough" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Bottom" },
			{ 2, "Top" },
			{ 3, "Left" },
			{ 4, "Right" },
			{ 5, "Center" },
			{ 6, "Hidden" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Enabled" },
			{ 2, "Disabled" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Primary" },
			{ 2, "Secondary" },
			{ 3, "Close" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "OffsetX" },
			{ 1, "OffsetY" },
			{ 2, "CrossFade" },
			{ 3, "Scale" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "Success" },
			{ 1, "NetworkError" },
			{ 2, "InvalidFormat" },
			{ 3, "Other" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "Success" },
			{ 1, "NetworkError" },
			{ 2, "InvalidFormat" },
			{ 3, "Other" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Mouse" },
			{ 2, "Touch" },
			{ 3, "Pen" },
			{ 4, "Keyboard" },
			{ 5, "GameController" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Committed" },
			{ 1, "Always" }
		},
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Projection" },
			{ 2, "NavigationDirectionDistance" },
			{ 3, "RectilinearDistance" }
		},
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Auto" },
			{ 2, "Projection" },
			{ 3, "NavigationDirectionDistance" },
			{ 4, "RectilinearDistance" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ -1, "Auto" },
			{ 0, "None" }
		},
		new Dictionary<int, string>
		{
			{
				int.MinValue,
				"Application"
			},
			{ -1, "Auto" },
			{ 0, "None" }
		},
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "ItemAdded" },
			{ 1, "ItemRemoved" },
			{ 2, "ActionCompleted" },
			{ 3, "ActionAborted" },
			{ 4, "Other" }
		},
		new Dictionary<int, string>
		{
			{ 0, "ImportantAll" },
			{ 1, "ImportantMostRecent" },
			{ 2, "All" },
			{ 3, "MostRecent" },
			{ 4, "CurrentThenMostRecent" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Normal" },
			{ 1, "Lower" },
			{ 2, "Upper" }
		},
		new Dictionary<int, string>
		{
			{ -1, "All" },
			{ 0, "None" },
			{ 1, "Bold" },
			{ 2, "Italic" },
			{ 4, "Underline" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Popup" },
			{ 1, "InPlace" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Hidden" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Off" },
			{ 2, "On" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Level1" },
			{ 2, "Level2" },
			{ 3, "Level3" },
			{ 4, "Level4" },
			{ 5, "Level5" },
			{ 6, "Level6" },
			{ 7, "Level7" },
			{ 8, "Level8" },
			{ 9, "Level9" }
		},
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Inserted" },
			{ 1, "Removed" },
			{ 2, "Edited" }
		},
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "TopLeft" },
			{ 2, "TopRight" },
			{ 3, "BottomLeft" },
			{ 4, "BottomRight" }
		},
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Arrow" },
			{ 1, "Cross" },
			{ 2, "Custom" },
			{ 3, "Hand" },
			{ 4, "Help" },
			{ 5, "IBeam" },
			{ 6, "SizeAll" },
			{ 7, "SizeNortheastSouthwest" },
			{ 8, "SizeNorthSouth" },
			{ 9, "SizeNorthwestSoutheast" },
			{ 10, "SizeWestEast" },
			{ 11, "UniversalNo" },
			{ 12, "UpArrow" },
			{ 13, "Wait" },
			{ 14, "Pin" },
			{ 15, "Person" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Standard" },
			{ 2, "Transient" },
			{ 3, "TransientWithDismissOnPointerMoveAway" }
		},
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "Added" },
			{ 1, "Removed" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Compact" },
			{ 2, "Inline" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Auto" },
			{ 1, "Default" },
			{ 2, "Disabled" }
		},
		null,
		new Dictionary<int, string>
		{
			{ 0, "InnerBorderEdge" },
			{ 1, "OuterBorderEdge" }
		},
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		null,
		new Dictionary<int, string>
		{
			{ 0, "None" },
			{ 1, "Cut" },
			{ 2, "Copy" },
			{ 3, "Paste" },
			{ 4, "SelectAll" },
			{ 5, "Delete" },
			{ 6, "Share" },
			{ 7, "Save" },
			{ 8, "Open" },
			{ 9, "Close" },
			{ 10, "Pause" },
			{ 11, "Play" },
			{ 12, "Stop" },
			{ 13, "Forward" },
			{ 14, "Backward" },
			{ 15, "Undo" },
			{ 16, "Redo" }
		},
		null,
		null,
		new Dictionary<int, string>
		{
			{ 1, "X" },
			{ 2, "Y" },
			{ 4, "Z" }
		},
		new Dictionary<int, string>
		{
			{ 0, "Top" },
			{ 1, "Left" }
		}
	};

	public static string GetNameForTypeID(int id)
	{
		if (id - 1 >= 0 && id - 1 < typeNames.Length)
		{
			return typeNames[id - 1];
		}
		return null;
	}

	public static string GetNameForPropertyID(int id)
	{
		if (id - 1 >= 0 && id - 1 < propertyNames.Length)
		{
			return propertyNames[id - 1];
		}
		return null;
	}

	public static string GetNameForEnumValue(int id, int value)
	{
		if (id - 572 >= 0 && id - 572 < enumValues.Length)
		{
			Dictionary<int, string> dictionary = enumValues[id - 572];
			if (dictionary == null)
			{
				return null;
			}
			if (dictionary.TryGetValue(value, out var value2))
			{
				return value2;
			}
			if (value == 0)
			{
				return null;
			}
			List<string> list = new List<string>();
			int num = value;
			foreach (int key in dictionary.Keys)
			{
				if (key != 0 && (num & key) == key)
				{
					list.Add(dictionary[key]);
					num &= ~key;
				}
			}
			if (num != 0)
			{
				return null;
			}
			return string.Join(",", list);
		}
		return null;
	}
}
