$command = $args[0]
$file=".\Directory.Build.props"
$xml = ([xml](Get-Content $file))
$version = ${xml}.Project.PropertyGroup.Version
$new = dotnet run --project .\Netsoft.Tools.Bump\Netsoft.Tools.Bump.csproj -- "$command" "$version"
${xml}.Project.PropertyGroup.Version = "$new"
$xml.Save($file)