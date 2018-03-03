@REM
@REM File: Pre-Compile.bat
@REM
@REM Summary: Builds all solutions.
@REM
@REM
@REM --------------------------------------------------------------------
@CALL "C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\2.0CommandPrompt.bat"

@CALL aspnet_compiler -p "G:\Sideprojects\naturalproducts\Solutions\Development\us.naturalproduct\NPCMemberWeb" -v / ".\Deployments\Staging"

@PAUSE