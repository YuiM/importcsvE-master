//import System;


/*==========================================================
 変数定義
==========================================================*/
//定数の宣言
var ForReading = 1; //読み込み
var ForWriting = 2; //書きこみ（上書きモード）
var ForAppending = 8; //書きこみ（追記モード）
 
var fileName = "";

	


/*==========================================================
 実行部
==========================================================*/




editFile();
 
ForReading = null;
ForWriting = null;
ForAppending = null; 
 
/*==========================================================
 関数定義
==========================================================*/
function editFile() {
 
  var strFolderPath;
  var objFileSys;
  var objInFile;
  var objOutFile;
  var strScriptPath;
  var strRecord;
 
  var index = 0;
 
  objFileSys = new ActiveXObject("Scripting.FileSystemObject");
  strScriptPath = String(WScript.ScriptFullName).replace(WScript.ScriptName,"");
 
 
  var args = new Array();

var oArgs = WScript.Arguments;
for (var tmpI = 0; tmpI < oArgs.length; tmpI++) {
   args[tmpI] = oArgs(tmpI);
}
oArgs = null;

for (var tmpA in args) {
   
   var fileName = args[tmpA];
   //WScript.Echo(fileName);
}


 
  objInFile = objFileSys.OpenTextFile(strScriptPath + fileName, ForReading);
  
  //objInFile = objFileSys.OpenTextFile(fileName, ForReading);
  try {
    var i, writingSize;
    do {
      index++;
      objOutFile = objFileSys.CreateTextFile(strScriptPath + "out" + index.toString() + ".txt", true);
 
      for (i = 0; i < 10000; i++) {
        strRecord = objInFile.ReadLine();
        objOutFile.WriteLine(strRecord);
        if (objInFile.AtEndOfStream == true) break;
      }
      objOutFile.Close();
    } while (objInFile.AtEndOfStream==false);
 
  } catch(e) {
    WScript.echo("Error!");
    WScript.echo(strScriptPath + "out" + index.toString() + ".txt");
  } finally {
    objInFile.Close();
    objOutFile.Close();
  }
 
  // オブジェクトの破棄
  objFileSys = null;
  objInFile = null;
  objOutFile = null;
  strScriptPath = null;
  strRecord = null;
  strFolderPath = null;
 
  return 0;
}