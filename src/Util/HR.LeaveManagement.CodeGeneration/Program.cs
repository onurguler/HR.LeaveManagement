using NJsonSchema.CodeGeneration.CSharp;

using NSwag;
using NSwag.CodeGeneration.CSharp;

var swaggerJsonContent =
    await File.ReadAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../../../API/HR.LeaveManagement.Api/swagger.json"));
var document = await OpenApiDocument.FromJsonAsync(swaggerJsonContent);

var settings = new CSharpClientGeneratorSettings
{
    ClassName = "ServiceClient",
    CSharpGeneratorSettings =
    {
        Namespace = "HR.LeaveManagement.BlazorUI.Services.Base", JsonLibrary = CSharpJsonLibrary.SystemTextJson,
    },
    UseBaseUrl = false,
    GenerateBaseUrlProperty = false,
    GenerateClientInterfaces = true,
};

var generator = new CSharpClientGenerator(document, settings);
var code = generator.GenerateFile();
await File.WriteAllTextAsync(
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../../../UI/HR.LeaveManagement.BlazorUI/Services/Base/ServiceClient.g.cs"),
    code);