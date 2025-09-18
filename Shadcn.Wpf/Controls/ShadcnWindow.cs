using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Shell;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;

namespace Shadcn.Wpf.Controls
{
    /// <summary>
    /// 基于 Window 和 WindowChrome 的 Shadcn 风格窗口控件
    /// 提供现代化的无边框窗口体验，支持自定义标题栏和系统按钮
    /// </summary>
    public class ShadcnWindow : Window
    {
        #region 私有字段
        
        private bool _isAnimating = false;
        private FrameworkElement? _contentContainer;
        
        #endregion

        #region 依赖属性

        /// <summary>
        /// 标题栏高度
        /// </summary>
        public static readonly DependencyProperty TitleBarHeightProperty =
            DependencyProperty.Register(nameof(TitleBarHeight), typeof(double), typeof(ShadcnWindow), 
                new PropertyMetadata(40.0));

        /// <summary>
        /// 是否显示标题栏
        /// </summary>
        public static readonly DependencyProperty ShowTitleBarProperty =
            DependencyProperty.Register(nameof(ShowTitleBar), typeof(bool), typeof(ShadcnWindow), 
                new PropertyMetadata(true));

        /// <summary>
        /// 是否显示系统按钮（最小化、最大化、关闭）
        /// </summary>
        public static readonly DependencyProperty ShowSystemButtonsProperty =
            DependencyProperty.Register(nameof(ShowSystemButtons), typeof(bool), typeof(ShadcnWindow), 
                new PropertyMetadata(true));

        /// <summary>
        /// 是否显示图标
        /// </summary>
        public static readonly DependencyProperty ShowIconProperty =
            DependencyProperty.Register(nameof(ShowIcon), typeof(bool), typeof(ShadcnWindow), 
                new PropertyMetadata(true));

        /// <summary>
        /// 标题栏内容
        /// </summary>
        public static readonly DependencyProperty TitleBarContentProperty =
            DependencyProperty.Register(nameof(TitleBarContent), typeof(object), typeof(ShadcnWindow));

        /// <summary>
        /// 是否启用窗口阴影
        /// </summary>
        public static readonly DependencyProperty EnableDropShadowProperty =
            DependencyProperty.Register(nameof(EnableDropShadow), typeof(bool), typeof(ShadcnWindow), 
                new PropertyMetadata(true));

        /// <summary>
        /// 最小化窗口命令
        /// </summary>
        public static readonly DependencyProperty MinimizeCommandProperty =
            DependencyProperty.Register(nameof(MinimizeCommand), typeof(ICommand), typeof(ShadcnWindow));

        /// <summary>
        /// 最大化/还原窗口命令
        /// </summary>
        public static readonly DependencyProperty MaximizeRestoreCommandProperty =
            DependencyProperty.Register(nameof(MaximizeRestoreCommand), typeof(ICommand), typeof(ShadcnWindow));

        /// <summary>
        /// 关闭窗口命令
        /// </summary>
        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register(nameof(CloseCommand), typeof(ICommand), typeof(ShadcnWindow));

        /// <summary>
        /// 是否启用窗口动画
        /// </summary>
        public static readonly DependencyProperty EnableAnimationsProperty =
            DependencyProperty.Register(nameof(EnableAnimations), typeof(bool), typeof(ShadcnWindow), 
                new PropertyMetadata(true));

        /// <summary>
        /// 动画持续时间
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register(nameof(AnimationDuration), typeof(Duration), typeof(ShadcnWindow), 
                new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(250))));

        #endregion

        #region 属性

        /// <summary>
        /// 标题栏高度
        /// </summary>
        public double TitleBarHeight
        {
            get => (double)GetValue(TitleBarHeightProperty);
            set => SetValue(TitleBarHeightProperty, value);
        }

        /// <summary>
        /// 是否显示标题栏
        /// </summary>
        public bool ShowTitleBar
        {
            get => (bool)GetValue(ShowTitleBarProperty);
            set => SetValue(ShowTitleBarProperty, value);
        }

        /// <summary>
        /// 是否显示系统按钮
        /// </summary>
        public bool ShowSystemButtons
        {
            get => (bool)GetValue(ShowSystemButtonsProperty);
            set => SetValue(ShowSystemButtonsProperty, value);
        }

        /// <summary>
        /// 是否显示图标
        /// </summary>
        public bool ShowIcon
        {
            get => (bool)GetValue(ShowIconProperty);
            set => SetValue(ShowIconProperty, value);
        }

        /// <summary>
        /// 标题栏内容
        /// </summary>
        public object TitleBarContent
        {
            get => GetValue(TitleBarContentProperty);
            set => SetValue(TitleBarContentProperty, value);
        }

        /// <summary>
        /// 是否启用窗口阴影
        /// </summary>
        public bool EnableDropShadow
        {
            get => (bool)GetValue(EnableDropShadowProperty);
            set => SetValue(EnableDropShadowProperty, value);
        }

        /// <summary>
        /// 是否启用窗口动画
        /// </summary>
        public bool EnableAnimations
        {
            get => (bool)GetValue(EnableAnimationsProperty);
            set => SetValue(EnableAnimationsProperty, value);
        }

        /// <summary>
        /// 动画持续时间
        /// </summary>
        public Duration AnimationDuration
        {
            get => (Duration)GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }

        /// <summary>
        /// 设置窗口内容并自动为动画创建包装容器
        /// </summary>
        public new object Content
        {
            get => base.Content;
            set => SetContentWithAnimation(value);
        }

        #endregion

        #region 命令

        /// <summary>
        /// 最小化窗口命令
        /// </summary>
        public ICommand MinimizeCommand
        {
            get => (ICommand)GetValue(MinimizeCommandProperty);
            private set => SetValue(MinimizeCommandProperty, value);
        }

        /// <summary>
        /// 最大化/还原窗口命令
        /// </summary>
        public ICommand MaximizeRestoreCommand
        {
            get => (ICommand)GetValue(MaximizeRestoreCommandProperty);
            private set => SetValue(MaximizeRestoreCommandProperty, value);
        }

        /// <summary>
        /// 关闭窗口命令
        /// </summary>
        public ICommand CloseCommand
        {
            get => (ICommand)GetValue(CloseCommandProperty);
            private set => SetValue(CloseCommandProperty, value);
        }

        #endregion

        #region 构造函数

        static ShadcnWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnWindow), 
                new FrameworkPropertyMetadata(typeof(ShadcnWindow)));
        }

        public ShadcnWindow()
        {
            InitializeCommands();
            
            // 设置高DPI感知和渲染选项
            SetRenderingOptions();
            
            // 监听窗口状态变化
            StateChanged += OnStateChanged;
            Loaded += OnLoaded;
            SourceInitialized += OnSourceInitialized;
            
            // 初始化动画状态
            InitializeAnimations();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 显示窗口并播放打开动画
        /// </summary>
        public new void Show()
        {
            base.Show();
        }

        /// <summary>
        /// 异步显示窗口并播放打开动画
        /// </summary>
        public new bool? ShowDialog()
        {
            return base.ShowDialog();
        }

        /// <summary>
        /// 关闭窗口并播放关闭动画
        /// </summary>
        public new void Close()
        {
            if (EnableAnimations)
            {
                PlayCloseAnimation(() => base.Close());
            }
            else
            {
                base.Close();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化命令
        /// </summary>
        private void InitializeCommands()
        {
            MinimizeCommand = new RelayCommand(ExecuteMinimize);
            MaximizeRestoreCommand = new RelayCommand(ExecuteMaximizeRestore);
            CloseCommand = new RelayCommand(ExecuteClose);
        }

        /// <summary>
        /// 初始化动画
        /// </summary>
        private void InitializeAnimations()
        {
            // 只设置初始透明度，RenderTransform在窗口加载后设置
            if (EnableAnimations)
            {
                Opacity = 0;
            }
        }

        /// <summary>
        /// 设置渲染选项以提高显示质量
        /// </summary>
        private void SetRenderingOptions()
        {
            // 启用高质量渲染
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.HighQuality);
            RenderOptions.SetEdgeMode(this, EdgeMode.Unspecified);
            RenderOptions.SetClearTypeHint(this, ClearTypeHint.Enabled);
            
            // 文本渲染优化
            TextOptions.SetTextFormattingMode(this, TextFormattingMode.Display);
            TextOptions.SetTextRenderingMode(this, TextRenderingMode.ClearType);
            TextOptions.SetTextHintingMode(this, TextHintingMode.Fixed);
            
            // 像素对齐
            SetValue(UseLayoutRoundingProperty, true);
            SetValue(SnapsToDevicePixelsProperty, true);
        }

        /// <summary>
        /// 设置内容并为动画创建包装容器
        /// </summary>
        private void SetContentWithAnimation(object content)
        {
            if (EnableAnimations && content != null && content is UIElement uiElement && content is not Border)
            {
                // 为内容创建包装容器用于动画
                var animationContainer = new Border
                {
                    Child = uiElement,
                    RenderTransformOrigin = new Point(0.5, 0.5)
                };
                
                _contentContainer = animationContainer;
                base.Content = animationContainer;
            }
            else
            {
                if (content is FrameworkElement element)
                {
                    _contentContainer = element;
                }
                base.Content = content;
            }
        }

        /// <summary>
        /// 播放窗口打开动画
        /// </summary>
        private void PlayOpenAnimation()
        {
            if (_isAnimating) return;
            _isAnimating = true;

            var fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = AnimationDuration,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            // 对内容容器应用缩放动画
            if (_contentContainer != null)
            {
                var scaleTransform = new ScaleTransform(0.9, 0.9);
                _contentContainer.RenderTransform = scaleTransform;
                _contentContainer.RenderTransformOrigin = new Point(0.5, 0.5);

                var scaleXAnimation = new DoubleAnimation
                {
                    From = 0.9,
                    To = 1.0,
                    Duration = AnimationDuration,
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                var scaleYAnimation = new DoubleAnimation
                {
                    From = 0.9,
                    To = 1.0,
                    Duration = AnimationDuration,
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
            }

            fadeInAnimation.Completed += (s, e) => _isAnimating = false;

            BeginAnimation(OpacityProperty, fadeInAnimation);
        }

        /// <summary>
        /// 播放窗口关闭动画
        /// </summary>
        private void PlayCloseAnimation(Action onCompleted)
        {
            if (_isAnimating) 
            {
                onCompleted?.Invoke();
                return;
            }
            _isAnimating = true;

            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };

            // 对内容容器应用缩放动画
            if (_contentContainer != null)
            {
                var scaleTransform = _contentContainer.RenderTransform as ScaleTransform;
                if (scaleTransform == null)
                {
                    scaleTransform = new ScaleTransform(1.0, 1.0);
                    _contentContainer.RenderTransform = scaleTransform;
                    _contentContainer.RenderTransformOrigin = new Point(0.5, 0.5);
                }

                var scaleXAnimation = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.9,
                    Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                };

                var scaleYAnimation = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.9,
                    Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                };

                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
            }

            fadeOutAnimation.Completed += (s, e) => 
            {
                _isAnimating = false;
                onCompleted?.Invoke();
            };

            BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        /// <summary>
        /// 窗口加载完成处理
        /// </summary>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // 设置DPI感知
            SetDpiAwareness();
            
            // 禁用DWM合成以避免圆角透明度问题
            DisableDwmBlur();
            
            // 如果启用动画且窗口刚创建，播放打开动画
            if (EnableAnimations && Opacity == 0)
            {
                PlayOpenAnimation();
            }
            
            // 注释掉Win32阴影调用，使用XAML中的DropShadowEffect代替
            // if (EnableDropShadow)
            // {
            //     ApplyDropShadow();
            // }
        }

        /// <summary>
        /// 窗口状态变化处理
        /// </summary>
        private void OnStateChanged(object? sender, EventArgs e)
        {
            // 动态设置WindowChrome的CornerRadius
            var chrome = WindowChrome.GetWindowChrome(this);
            if (chrome != null)
            {
                // 创建新的WindowChrome实例以避免修改共享资源
                var newChrome = new WindowChrome
                {
                    CaptionHeight = chrome.CaptionHeight,
                    CornerRadius = WindowState == WindowState.Maximized ? new CornerRadius(0) : new CornerRadius(8),
                    GlassFrameThickness = chrome.GlassFrameThickness,
                    ResizeBorderThickness = chrome.ResizeBorderThickness,
                    UseAeroCaptionButtons = chrome.UseAeroCaptionButtons
                };
                WindowChrome.SetWindowChrome(this, newChrome);
            }
            
            // 同时设置系统级别的圆角
            try
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                if (hwnd != IntPtr.Zero)
                {
                    var preference = WindowState == WindowState.Maximized 
                        ? (int)DwmWindowCornerPreference.DWMWCP_DONOTROUND 
                        : (int)DwmWindowCornerPreference.DWMWCP_ROUND;
                    
                    DwmSetWindowAttribute(hwnd, DwmWindowAttribute.DWMWA_WINDOW_CORNER_PREFERENCE, 
                        ref preference, Marshal.SizeOf<int>());
                }
            }
            catch
            {
                // 忽略设置失败
            }
        }

        /// <summary>
        /// 窗口源初始化处理
        /// </summary>
        private void OnSourceInitialized(object? sender, EventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd != IntPtr.Zero)
            {
                var source = HwndSource.FromHwnd(hwnd);
                source?.AddHook(WndProc);
            }
        }

        /// <summary>
        /// 窗口消息处理，处理最大化时的工作区边界
        /// </summary>
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_GETMINMAXINFO = 0x0024;

            if (msg == WM_GETMINMAXINFO)
            {
                var mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);
                
                // 获取当前显示器的工作区域
                var monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
                if (monitor != IntPtr.Zero)
                {
                    var monitorInfo = new MONITORINFO();
                    monitorInfo.cbSize = Marshal.SizeOf<MONITORINFO>();
                    
                    if (GetMonitorInfo(monitor, ref monitorInfo))
                    {
                        var workArea = monitorInfo.rcWork;
                        var monitorArea = monitorInfo.rcMonitor;
                        
                        mmi.ptMaxPosition.X = workArea.Left - monitorArea.Left;
                        mmi.ptMaxPosition.Y = workArea.Top - monitorArea.Top;
                        mmi.ptMaxSize.X = workArea.Right - workArea.Left;
                        mmi.ptMaxSize.Y = workArea.Bottom - workArea.Top;
                    }
                }
                
                Marshal.StructureToPtr(mmi, lParam, true);
                handled = true;
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// 应用窗口阴影
        /// </summary>
        private void ApplyDropShadow()
        {
            try
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                if (hwnd != IntPtr.Zero)
                {
                    SetWindowDropShadow(hwnd);
                }
            }
            catch
            {
                // 忽略阴影设置失败
            }
        }

        #endregion

        #region 命令实现

        private void ExecuteMinimize()
        {
            WindowState = WindowState.Minimized;
        }

        private void ExecuteMaximizeRestore()
        {
            WindowState = WindowState == WindowState.Maximized 
                ? WindowState.Normal 
                : WindowState.Maximized;
        }

        private void ExecuteClose()
        {
            var args = new CancelEventArgs();
            OnClosing(args);
            
            if (!args.Cancel)
            {
                Close();
            }
        }

        #endregion

        /// <summary>
        /// 设置DPI感知
        /// </summary>
        private void SetDpiAwareness()
        {
            try
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                if (hwnd != IntPtr.Zero)
                {
                    // 启用DPI感知
                    var source = PresentationSource.FromVisual(this);
                    if (source?.CompositionTarget != null)
                    {
                        var dpiX = 96.0 * source.CompositionTarget.TransformToDevice.M11;
                        var dpiY = 96.0 * source.CompositionTarget.TransformToDevice.M22;
                        
                        // 确保窗口在高DPI下正确显示
                        SetDpiForWindow(hwnd);
                    }
                }
            }
            catch
            {
                // 忽略DPI设置失败
            }
        }

        /// <summary>
        /// 为窗口设置DPI
        /// </summary>
        private void SetDpiForWindow(IntPtr hwnd)
        {
            try
            {
                // 设置窗口DPI感知模式
                var preference = (int)DwmWindowCornerPreference.DWMWCP_ROUND;
                DwmSetWindowAttribute(hwnd, DwmWindowAttribute.DWMWA_WINDOW_CORNER_PREFERENCE, 
                    ref preference, Marshal.SizeOf<int>());
            }
            catch
            {
                // 忽略设置失败
            }
        }
        private void DisableDwmBlur()
        {
            try
            {
                var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
                if (hwnd != IntPtr.Zero)
                {
                    var accent = new AccentPolicy
                    {
                        AccentState = AccentState.ACCENT_DISABLED,
                        GradientColor = 0
                    };

                    var accentStructSize = Marshal.SizeOf(accent);
                    var accentPtr = Marshal.AllocHGlobal(accentStructSize);
                    Marshal.StructureToPtr(accent, accentPtr, false);

                    var data = new WindowCompositionAttributeData
                    {
                        Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                        SizeOfData = accentStructSize,
                        Data = accentPtr
                    };

                    SetWindowCompositionAttribute(hwnd, ref data);
                    Marshal.FreeHGlobal(accentPtr);
                }
            }
            catch
            {
                // 忽略错误，保持向后兼容性
            }
        }

        #region Win32 API

        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute attr, ref int attrValue, int attrSize);

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("user32.dll")]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        private const uint MONITOR_DEFAULTTONEAREST = 2;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        private enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        internal enum DwmWindowAttribute
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        internal enum DwmWindowCornerPreference
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        private enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            ACCENT_INVALID_STATE = 5
        }

        /// <summary>
        /// 设置窗口阴影
        /// </summary>
        private void SetWindowDropShadow(IntPtr hwnd)
        {
            var accent = new AccentPolicy
            {
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND
            };

            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(hwnd, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }

        #endregion
    }
}
