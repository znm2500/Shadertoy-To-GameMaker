[中文](#shadertoy-to-gamemaker-shadertoy-到-gamemaker-转换器)

# Shadertoy to Gamemaker

A desktop utility built with C# and WPF to convert shaders from [Shadertoy](https://www.shadertoy.com) into a format compatible with [GameMaker](https://gamemaker.io/). This tool automates many of the tedious conversion steps, including uniform mapping, coordinate adjustments, and generating the necessary GameMaker Language (GML) event code.

## Web Version (`index.html`)

A powerful, full-featured web version of this tool is available online. You can access it directly at: [https://znm2500.github.io/Shadertoy-To-GameMaker/](https://znm2500.github.io/Shadertoy-To-GameMaker/)

Alternatively, you can open the `index.html` file in your browser to use it locally.

The web version offers a streamlined user experience with a step-by-step wizard and includes all the features of the desktop app, plus:

*   **Localization:** Switch between English and Chinese interfaces.
*   **Asset Package Export:** Generates a complete GameMaker Asset Package (`.yymps`) containing the shader, the object, any required sprites, and all configuration files. This allows for easy one-click import into your GameMaker project.
*   **Drag-and-Drop:** Easily add textures for channels by dragging and dropping image files.

## Key Features (Desktop Version)

*   **Advanced GLSL Conversion:** Converts modern GLSL from Shadertoy to GLSL ES compatible with GameMaker.
*   **Preprocessor Support:** Correctly handles `#define`, `#if`, `#ifdef`, `#ifndef`, `#elif`, and `#else` directives, allowing for the conversion of complex, multi-version shaders.
*   **Function Overload Resolution:** Automatically renames overloaded functions (e.g., `myFunc()` -> `myFunc1()`, `myFunc2()`) to prevent compilation errors in GameMaker.
*   **Automatic Uniform Mapping:**
    *   Converts standard Shadertoy uniforms (`iResolution`, `iTime`, `iMouse`, `iFrame`) to their GameMaker equivalents (`gm_pSurfaceDimensions`, `gm_pTime`, etc.).
    *   Parses custom `uniform` declarations and generates the GML code required to create and update them.
*   **iChannel Configuration:** A user-friendly interface to map shader input channels (`iChannel0` to `iChannel3`) to GameMaker resources like sprites, surfaces, or the main application surface.
*   **GML Code Generation:** Creates all the necessary GML code for a GameMaker object, including the `Create`, `Clean Up`, and `Draw` events, to get the shader running quickly.
*   **Multi-Pass Shader Support:** Handles multi-pass shaders by allowing you to define and convert code for `Buffer A` through `Buffer D` and the final `Image` pass.

## How to Use (Desktop Version)

1.  **Launch the application** (`Shadertoy to Gamemaker.exe`).
2.  **Initial Setup:** Enter a base name for your shader assets (e.g., `shd_fire`).
3.  **Open Editor:** Proceed to the main editor window.
4.  **Paste Code:** Paste your Shadertoy GLSL code into the input panel for the desired pass (e.g., `Image`, `Buffer A`).
5.  **Configure Channels:** If the shader uses `iChannel` inputs, configure each one using the dropdown menus and text fields.
6.  **Convert:** Click the "Convert" button.
7.  **Copy Assets:** The tool will generate the converted GLSL `.fsh` code and the GML for the `Create`, `Draw`, and `Clean Up` events.
8.  **Implement in GameMaker:** Create a new shader asset in your GameMaker project, paste in the `.fsh` code. Then, create an object and copy the generated GML into the corresponding events.

## Building from Source (Desktop Version)

### Prerequisites

*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   Visual Studio 2022 or later

### Steps

1.  Clone this repository.
2.  Open `Shadertoy to Gamemaker.sln` in Visual Studio.
3.  Build the solution (F6 or `Build > Build Solution`). The executable will be located in `Shadertoy to Gamemaker/bin/Debug/net8.0-windows/`.

---

[English](#shadertoy-to-gamemaker)

# Shadertoy to Gamemaker (Shadertoy 到 GameMaker 转换器)

一个使用 C# 和 WPF 构建的桌面应用程序，用于将 [Shadertoy](https://www.shadertoy.com) 网站上的着色器转换为与 [GameMaker](https://gamemaker.io/) 引擎兼容的格式。该工具能自动完成许多繁琐的转换步骤，包括 uniform 映射、坐标系调整以及生成配套的 GameMaker Language (GML) 事件代码。

## 网页版 (`index.html`)

本项目的功能强大的网页版已上线。您可以通过以下链接直接访问：[https://znm2500.github.io/Shadertoy-To-GameMaker/](https://znm2500.github.io/Shadertoy-To-GameMaker/)

或者，您也可以直接在浏览器中打开 `index.html` 文件在本地使用。

网页版提供了一个流线型的分步向导界面，包含了桌面版的所有功能，并额外增加了：

*   **本地化:** 可在中英文界面之间切换。
*   **资源包导出:** 能生成一个完整的 GameMaker 资源包 (`.yymps`)，其中包含了着色器、对象、所需的 Sprite 以及所有配置文件。这允许您一键将其导入到 GameMaker 项目中。
*   **拖放操作:** 通过拖放图像文件，轻松地为通道添加纹理。

## 核心功能 (桌面版)

*   **高级 GLSL 转换:** 将 Shadertoy 使用的现代 GLSL 转换为与 GameMaker 兼容的 GLSL ES。
*   **预处理器支持:** 正确处理 `#define`, `#if`, `#ifdef`, `#ifndef`, `#elif`, 和 `#else` 指令，使其能够转换复杂的多版本着色器。
*   **函数重载解析:** 自动重命名重载的函数（例如 `myFunc()` -> `myFunc1()`, `myFunc2()`），以避免在 GameMaker 中出现编译错误。
*   **自动 Uniform 映射:**
    *   将标准的 Shadertoy uniform (`iResolution`, `iTime`, `iMouse`, `iFrame`) 转换为 GameMaker 的内置等效项 (`gm_pSurfaceDimensions`, `gm_pTime` 等)。
    *   解析用户自定义的 `uniform` 声明，并生成创建和更新它们所需的 GML 代码。
*   **iChannel 配置:** 提供用户友好的界面，用于将着色器的输入通道 (`iChannel0` 到 `iChannel3`) 映射到 GameMaker 的资源，如 sprite（精灵）、surface（表面）或主应用 surface。
*   **GML 代码生成:** 为 GameMaker 对象生成所有必需的 GML 代码，包括 `Create` (创建)、`Clean Up` (清理) 和 `Draw` (绘制) 事件，帮助您快速运行着色器。
*   **多通道着色器支持:** 支持处理多通道着色器，允许您为 `Buffer A` 到 `Buffer D` 以及最终的 `Image` 通道分别定义和转换代码。

## 如何使用 (桌面版)

1.  **启动应用程序** (`Shadertoy to Gamemaker.exe`)。
2.  **初始设置:** 为您的着色器资源输入一个基础名称（例如 `shd_fire`）。
3.  **打开编辑器:** 进入主编辑器窗口。
4.  **粘贴代码:** 将您的 Shadertoy GLSL 代码粘贴到目标通道（例如 `Image`, `Buffer A`）的输入面板中。
5.  **配置通道:** 如果着色器使用了 `iChannel` 输入，请使用下拉菜单和文本框对每个通道进行配置。
6.  **转换:** 点击“转换”按钮。
7.  **复制代码资源:** 工具将生成转换后的 GLSL `.fsh` 代码以及用于 `Create`、`Draw` 和 `Clean Up` 事件的 GML 代码。
8.  **在 GameMaker 中实施:** 在您的 GameMaker 项目中创建一个新的着色器资源，并粘贴 `.fsh` 代码。然后，创建一个对象，并将生成的 GML 复制到相应的事件中。

## 从源码构建 (桌面版)

### 环境要求

*   [.NET 8.0 SDK](https.dotnet.microsoft.com/download/dotnet/8.0)
*   Visual Studio 2022 或更高版本

### 步骤

1.  克隆此仓库。
2.  在 Visual Studio 中打开 `Shadertoy to Gamemaker.sln`。
3.  构建解决方案（按 F6 或选择 `生成 > 生成解决方案`）。生成的可执行文件将位于 `Shadertoy to Gamemaker/bin/Debug/net8.0-windows/` 目录下。
