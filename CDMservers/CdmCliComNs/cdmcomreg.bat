

if exist "%windir%\SysWOW64" ( C:\Windows\Microsoft.NET\Framework64\v4.0.30319\regasm YunYiCdm.dll /codebase /tlb:YunYiCdm.tlb ) else ( C:\Windows\Microsoft.NET\Framework\v4.0.30319\regasm YunYiCdm.dll /codebase /tlb:YunYiCdm.tlb )
pause