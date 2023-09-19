# Instruction pour obfuscar (Windows seulement)
Obfuscar a été testé avec Mac et ça ne fonctionne pas sans certaines dll.

Pour faire fonctionner Obfuscar sur windows, télécharger le package obfuscar , ça placer tous les fichiers d'execution
dans le fichier ~/.nuget/obfuscar

Pour ajouter l'étape d'obfuscation automatiquement après chaque build. Il faut ajouter un post build event ainsi qu'un fichier xml dans 
le répertoire du projet.

Exemple de post build event (à ajouter dans le .csproj.
```
<Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="cd $(TargetDir)&#xD;&#xA;$(Obfuscar) .\{Nom du fichier}.xml" />
  </Target>
```
C'est aussi possible d'ajouter des règles de génération selon la version de build. Exemple : obfuscer seulment lorsqu'en release

Le fichier xml doit ressembler à ça :
```
<?xml version='1.0'?>
<Obfuscator>
  <Var name="InPath" value="." />
  <Var name="OutPath" value="$(InPath)\obfuscated" />
  <Var name="KeepPublicApi" value="false" />
  <Var name="HidePrivateApi" value="true" />
  <Var name="RenameProperties" value="true" />
  <Var name="RenameEvents" value="true" />
  <Var name="RenameFields" value="true" />
  <Var name="UseUnicodeNames" value="true" />
  <Var name="HideStrings" value="true" />
  <Var name="OptimizeMethods" value="true" />
  <Var name="SuppressIldasm" value="true" />
  <Module file="$(InPath)\{Nom du projet}.dll" />
</Obfuscator>
```

Ceci est le minimum pour faire fonctionner l'obfuscation mais il se pourrait que l'obfuscation ne fonctionne quand même pas.
Ex : Missing Microsoft.AspNetCore.MvcCore.dll
Dans ce cas, il faut copier les dépendences indiqué dans le répertoire d'output. Dans mon cas c'était dans \bin\{Version}\net6.0


