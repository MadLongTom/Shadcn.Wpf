# Shadcn.Wpf 设计令牌系统

## 概述

本文档定义了 Shadcn.Wpf 组件库的设计令牌系统，确保所有组件使用一致的设计规范。

## 设计令牌分类

### 1. 间距系统 (Spacing System)

#### 基础间距值
- `Space0` - `Space32`: 0px 到 128px 的数值型间距
- `Spacing0` - `Spacing32`: 对应的 Thickness 类型间距

#### 方向性间距
- **顶部间距**: `SpacingTopXs` 到 `SpacingTopXl`
- **底部间距**: `SpacingBottomXs` 到 `SpacingBottomXl`
- **左侧间距**: `SpacingLeftXs` 到 `SpacingLeftXl`
- **右侧间距**: `SpacingRightXs` 到 `SpacingRightXl`
- **水平间距**: `SpacingHorizontalXs` 到 `SpacingHorizontalXl`
- **垂直间距**: `SpacingVerticalXs` 到 `SpacingVerticalXl`

#### 组件专用间距
- **组件内边距**: `ComponentPaddingXs` 到 `ComponentPaddingXl`
- **组件外边距**: `ComponentMarginXs` 到 `ComponentMarginXl`
- **图标边距**: `IconMarginXs` 到 `IconMarginMd`
- **内容边距**: `ContentMarginSm` 到 `ContentMarginLg`

### 2. 尺寸系统 (Size System)

#### 图标尺寸
- `IconSizeXs`: 12px
- `IconSizeSm`: 16px
- `IconSizeMd`: 20px
- `IconSizeLg`: 24px
- `IconSizeXl`: 32px

#### 控件高度
- `ControlHeightSm`: 32px
- `ControlHeightMd`: 36px
- `ControlHeightLg`: 44px
- `ControlHeightXl`: 48px

### 3. 边框系统 (Border System)

#### 圆角半径
- `RadiusNone`: 0
- `RadiusSm`: 2px
- `RadiusBase`: 4px
- `RadiusMd`: 6px
- `RadiusLg`: 8px
- `RadiusXl`: 12px
- `Radius2Xl`: 16px
- `Radius3Xl`: 24px
- `RadiusFull`: 9999px

#### 边框厚度
- `BorderNone`: 0
- `BorderThin`: 1px
- `BorderMedium`: 2px
- `BorderThick`: 3px

#### 特定边框
- `BorderBottom`: 底部边框
- `BorderTop`: 顶部边框
- `BorderLeft`: 左侧边框
- `BorderRight`: 右侧边框

### 4. 字体系统 (Typography System)

#### 字体大小
- `FontSizeXs`: 12px
- `FontSizeSm`: 14px
- `FontSizeMd`: 16px
- `FontSizeBase`: 16px
- `FontSizeLg`: 18px
- `FontSizeXl`: 20px
- `FontSize2Xl`: 24px
- `FontSize3Xl`: 30px
- `FontSize4Xl`: 36px

#### 字体族
- `DefaultFontFamily`: Geist, Microsoft YaHei, 微软雅黑, Segoe UI, Tahoma, Arial, Helvetica, sans-serif
- `MonoFontFamily`: Consolas, Courier New, 等线, ui-monospace, monospace

#### 字体粗细
- `FontWeightThin` 到 `FontWeightBlack`: 完整的字体粗细范围

## 组件标准化状态

### ✅ 已完成标准化的组件

#### ShadcnButton
- **间距**: 使用 `ComponentPaddingMd`、`IconMarginXs`
- **尺寸**: 使用 `ControlHeightMd`、`RadiusMd`
- **边框**: 使用 `BorderThin`
- **变体支持**: Default, Destructive, Outline, Secondary, Ghost, Link
- **尺寸支持**: Small, Medium, Large, Icon

#### ShadcnTextBox
- **间距**: 使用 `ComponentPaddingMd`、`SpacingTopXs`
- **尺寸**: 使用 `ControlHeightMd`、`RadiusMd`
- **边框**: 使用 `BorderMedium`、`BorderNone`
- **字体**: 使用 `FontSizeXs`、`FontSizeSm`、`FontSizeMd`
- **尺寸支持**: Small, Medium, Large

#### ShadcnCheckBox
- **间距**: 使用 `ContentMarginSm`、`SpacingTopXs`、`ComponentMarginXs`
- **尺寸**: 使用 `IconSizeSm`、`IconSizeXs`、`IconSizeLg`
- **边框**: 使用 `BorderMedium`、`RadiusSm`
- **字体**: 使用 `FontSizeXs`、`FontSizeSm`、`FontSizeMd`
- **尺寸支持**: Small, Medium, Large

### 🔄 待标准化的组件

- ShadcnRadioButton
- ShadcnComboBox
- ShadcnListBox
- ShadcnProgressBar
- ShadcnTabControl
- ShadcnToggleSwitch
- ShadcnCard
- ShadcnNavMenu
- ShadcnWindow

## 使用指南

### 1. 新组件开发
创建新组件时，必须使用设计令牌而非硬编码值：

```xml
<!-- ❌ 错误 - 硬编码值 -->
<Setter Property="Padding" Value="16,8"/>
<Setter Property="MinHeight" Value="36"/>

<!-- ✅ 正确 - 使用设计令牌 -->
<Setter Property="Padding" Value="{DynamicResource ComponentPaddingMd}"/>
<Setter Property="MinHeight" Value="{DynamicResource ControlHeightMd}"/>
```

### 2. 组件变体
所有组件应支持标准变体系统：
- **尺寸变体**: Xs, Sm, Md, Lg, Xl
- **视觉变体**: Default, Secondary, Destructive, Outline, Ghost
- **状态变体**: Normal, Hover, Active, Disabled, Focus

### 3. 响应式设计
使用 `DynamicResource` 确保主题切换时的响应性：

```xml
<!-- 支持主题切换 -->
<Setter Property="Background" Value="{DynamicResource PrimaryBrush}"/>
```

## 维护指南

### 1. 添加新令牌
新的设计令牌应添加到相应的资源文件：
- **间距/尺寸**: `Layout.xaml`
- **颜色**: `Colors.xaml`
- **字体**: `Typography.xaml`

### 2. 版本控制
设计令牌的更改应该：
- 保持向后兼容性
- 记录变更原因
- 更新相关文档

### 3. 测试
每次令牌更改后应测试：
- 所有组件的视觉一致性
- 主题切换功能
- 响应式行为

## 最佳实践

1. **一致性优先**: 始终使用设计令牌而非硬编码值
2. **语义化命名**: 令牌名称应反映其用途而非具体值
3. **分层设计**: 基础令牌 → 语义令牌 → 组件令牌
4. **文档同步**: 保持代码与文档的同步更新

## 下一步计划

1. 完成剩余组件的标准化
2. 建立自动化测试确保令牌使用的一致性
3. 创建设计令牌的可视化文档
4. 实现令牌的运行时动态修改功能
