<html>
<head>
<title>
Show Tables
</title>
</head>
<body>
<center>
<form action="print_table.php" method="POST">
<h1><b>Enter the name of the table you want to access:</b></h1>
<p>
<?php

function prtable($table) {
	print "<tr><select name=\"table\">";
        while ($a_row = mysqli_fetch_row($table)) {
                foreach ($a_row as $field) print "<option value=\"$field\">$field</option>";
        }
	print "<br><br>";
}

require("../dbguest.php");
$link = mysqli_connect($host, $user, $pass, $db);
if (!$link) die("Couldn't connect to MySQL");


mysqli_select_db($link, $db)
        or die("Couldn't open $db: ".mysqli_error($link));

$table = "users";
$result = mysqli_query($link, "show tables");
$num_rows = mysqli_num_rows($result);
print "<br><h3>There are $num_rows  tables</h3><br>";

prtable($result);

mysqli_close($link);

?>
<br>
<input type="submit" value="submit">
</form>
<br>
<a href="main.php"> back to MAIN menu</a>
</p>
</center>
</body>

</html>

