<html>
<head>
<title>
print_table.php
</title>
</head>
<body>
<center>
<?php

$table = $_POST["table"];

function prtable($table) {
#	print "<table border=1>\n";
	while ($a_row = mysqli_fetch_row($table)) {
		print "<tr>";
		foreach ($a_row as $field) print "<td>$field</td>";
		print "</tr>";
	}
	print "</table>";
}
function prcol($tab,$table) {
	print "<table border=1>\n";
	print "<tr>";
        while ($a_row = mysqli_fetch_row($tab)) {
                foreach ($a_row as $field) print "<td>$field</td>";
        }
	if($table == "TRANSACTION") print "<td>Total Amount</td>";
	print "</tr>";
}
require("../dbguest.php");

$link = mysqli_connect($host, $user, $pass);
if (!$link) die("Couldn't connect to MySQL");
print "Successfully connected to server<p>";

mysqli_select_db($link, $db)
	or die("Couldn't open $db: ".mysqli_error($link));
print "Successfully selected database \"$db\"<p>";

$result = mysqli_query($link, "select * from $table");

if (!$result) print("ERROR: ".mysqli_error($link));
else {
    $num_rows = mysqli_num_rows($result);
    print "There are $num_rows rows in the table<p>";
    if($table == "TRANSACTION"){
	$result = mysqli_query($link, "SELECT TRANSACTION.Trxn_Number,TRANSACTION.Date ,TRANSACTION.CID ,TRANSACTION.Discount_Code ,(SUM(ITEM.Price * TRANSACION_DETAILS.Quantity * 1 - 2.5 * TRANSACTION.Discount_Code / 100)) AS total FROM TRANSACTION, TRANSACION_DETAILS, ITEM WHERE ( ITEM.ID = TRANSACION_DETAILS.ID AND TRANSACION_DETAILS.Trxn_Number = TRANSACTION.Trxn_Number) GROUP BY TRANSACTION.Trxn_Number");		
    } else {
        $result = mysqli_query($link, "select * from $table");
    }

$col = mysqli_query($link, "select column_name from information_schema.columns where table_name='$table'");
if (!$col) print("ERROR: ".mysqli_error($link));
else {
    prcol($col,$table);
}
    prtable($result);
}

mysqli_close($link);

?>

<p>
<a href="show_tables.php"> back </a><br>
<a href="main.php"> back to MAIN menu</a>
</center>
</body>
</html>


