
:if exist "%windir%\SysWOW64" ( C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil /u VoicePlayService.exe ) else ( C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil /u VoicePlayService.exe )
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil /u VoicePlayService.exe 
pause