
$scriptPath = $MyInvocation.MyCommand.Path

Get-ChildItem -Recurse -File | ForEach-Object {
    $path = $_.FullName
    $lines = Get-Content $path
    $output = @()
    $inIncomingChange = $true

    if (($path -notmatch '\.(prefab)$') -or ($path -eq $scriptPath -or $path -like "*.vscode*" -or $path -like "*.git*" -or $path -like "*Library*" -or $path -match '\.(png|psd|fbx|mat|tif|mp3|unity|exr|wav)$')) {
        return # Переходим к следующему файлу
    }
    Write-Host "Читаем файл: $path"
    foreach ($line in $lines) {
        if ($line -match '<<<<<<< HEAD') {
            $inIncomingChange = $false
            Write-Host "encountered HEAD at $path"
            continue
        } elseif ($line -match '=======') {
            $inIncomingChange = $true
            Write-Host "encountered ======= at $path"
            continue
        } elseif ($line -match '^>>>>>>>') {
            Write-Host "encountered >>>>>>> at $path"
            continue
        } elseif( $inIncomingChange){
            
            $output += $line
        }
    }

    # Write the cleaned content back to the file
    $output | Set-Content $path 
} 

Write-Host "✔ Done"