function Run {
	
	dotnet format

	StopIfError "Format error"

	dotnet build -c Debug

	StopIfError "Build error"

	dotnet test -c Debug

	StopIfError "Test error"

}

function StopIfError{
	param(
		$message
	)

	if($?)
	{
		return
	}

	Write-Error $message
}

$ErrorActionPreference = "Stop"
Run
