@Echo Off

csc /target:library /out:bin/ThunderMain.URLRewriter.dll rewriter.cs
Echo Compilation Complete
Echo.
Echo Now install into the GAC by dragging and dropping bin/ThunderMain.URLRewriter.dll into C:\Windows\assembly
Echo.
pause