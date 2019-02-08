<html>
<head><title>Add Article</title></head>
<body>
<?php
require("../dbguest.php");
if( isset($_POST['submit']) ){
    //be sure to validate and clean your variables
    $title = htmlentities($_POST['title']);
    $pgst = htmlentities($_POST['pgst']);
    $pgend = htmlentities($_POST['pgend']);
    $magazine = htmlentities($_POST['magazine']);
    $author = $_POST['author'];
    // Create connection
    $conn = mysqli_connect($host, $user, $pass, $db);
    // Check connection
    if (!$conn) {
        die("Connection failed: " . mysqli_connect_error());
    }

    if($title == NULL or $pgst == NULL or $pgend == NULL or $magazine == NULL){
        echo "<script> alert(\"Please fill all the values \")</script>";
    } else {

        $sql = "INSERT INTO ARTICLE (Title, Page_Start, Page_End, Magazine_ID) VALUES (\"".$title."\",\"".$pgst."\",\"".$pgend."\",\"".$magazine. "\")";

        if (mysqli_multi_query($conn, $sql)) {
            echo "<center>New records created successfully<center>";
#	    header("Location: login.php");
#            exit;
        } else {
	    echo "<center> Error: " . $sql . "<br>" . mysqli_error($conn) ."</center>";

#            echo "<script> alert(\"Error: There is a problem with insertion  \") </script>";
        }
	$res = mysqli_query($conn, "SELECT max(Article_ID) FROM ARTICLE");
        $row = mysqli_fetch_row($res);
        $arti = $row[0];
	foreach($author as $auth){

           $sql = "INSERT INTO WRITES (Author_ID,Article_ID) VALUES ($auth,$arti)";

           if (mysqli_multi_query($conn, $sql)) {

           } else {
               echo "<center> Error: " . $sql . "<br>" . mysqli_error($conn) ."</center>";

#            echo "<script> alert(\"Error: There is a problem with insertion  \") </script>";
           }
        }
        

    }

mysqli_close($conn);
}
?>

<center>
<h1>Add Article</h1>
<br><br><br>
<h3> please add Articles filling up the below form </h3>
<form name ="signup" action="" method="post">
<table name="data"> 
<tr>
<th>Title :</th><th><input name="title" type="text" ></th>
</tr>
<tr>
<th>Page Start :</th><th><input name="pgst" type="text" ></th>
</tr>
<tr>
<th>Page End :</th><th><input name="pgend" type="text" ></th>
</tr>
<tr>
<th>Magazine :</th><th>
<select name="magazine">
<?php

function prtable($table) {
#        print "<select name=\"magazine\">\n";
        while ($a_row = mysqli_fetch_row($table)) {
		print "<option value=\"$a_row[0]\">$a_row[1] Vol:$a_row[2]</option>";
        }
#        print "</select>";
}
require("../dbguest.php");
$link = mysqli_connect($host, $user, $pass, $db);
if (!$link) die("Couldn't connect to MySQL");

mysqli_select_db($link, $db)
        or die("Couldn't open $db: ".mysqli_error($link));
#print "Successfully selected database \"$db\"<p>";

$table = "users";
$result = mysqli_query($link, "SELECT Maganize_ID,Title,Volume FROM MAGAZINE");

prtable($result);

mysqli_close($link);

?>
</select>
</th>
</tr>
<tr>
<th>Author :</th><th>
<select name="author[]" multiple>
<?php
function prntable($table) {
#        print "<select name=\"magazine\">\n";
        while ($a_row = mysqli_fetch_row($table)) {
                print "<option value=\"$a_row[2]\">$a_row[0] $a_row[1]</option>";
        }
#        print "</select>";
}
require("../dbguest.php");
$link = mysqli_connect($host, $user, $pass, $db);
if (!$link) die("Couldn't connect to MySQL");

mysqli_select_db($link, $db)
        or die("Couldn't open $db: ".mysqli_error($link));
#print "Successfully selected database \"$db\"<p>";

$table = "users";
$result = mysqli_query($link, "SELECT A_Name,A_Last,Author_ID FROM AUTHOR");

prntable($result);

mysqli_close($link);

?>
</select>
</th>
</tr>
</table>
<input type="submit" value="submit" name="submit"> <input type="reset" value="reset" name="reset">
</form>
<a href="main.php"> back to MAIN menu</a>
</center>
</body>
</html>

