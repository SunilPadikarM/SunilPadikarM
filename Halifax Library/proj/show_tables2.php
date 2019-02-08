<html>
<head>
<title>
Show Tables
</title>
</head>
<body>
<center>
<form action="print_table.php" method="POST">
<b>Enter the name of the table you want to access:</b>
<input type="text" name="table">
<p>
<input type="submit" value="submit">
</form>
<?php

function prtable($table) {
        print "<table border=1>\n";
	print "<tr><td>Name Of The Table</td></tr>";
        while ($a_row = mysqli_fetch_row($table)) {
                print "<tr>";
                foreach ($a_row as $field) print "<td>$field</td>";
                print "</tr>";
        }
        print "</table>";
}

require("../dbguest.php");
$link = mysqli_connect($host, $user, $pass, $db);
if (!$link) die("Couldn't connect to MySQL");
print "Successfully connected to server<p>";


mysqli_select_db($link, $db)
        or die("Couldn't open $db: ".mysqli_error($link));
#print "Successfully selected database \"$db\"<p>";

$table = "users";
$result = mysqli_query($link, "show tables");
$num_rows = mysqli_num_rows($result);
print "There are $num_rows  tables<p>";

prtable($result);

mysqli_close($link);

?>
<a href="main.php"> back to MAIN menu</a>
</center>
</body>

</html>

