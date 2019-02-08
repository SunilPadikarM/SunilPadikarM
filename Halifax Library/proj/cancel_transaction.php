<html>
<head>
<title>
Cancel Transaction
</title>
</head>
<body>

<?php

if(isset($_POST['submit']) ){


//It will get value of table which is passed in the textbox 
$trxn_number = $_POST["trxn_number"];


require("/home/student_2018_fall/g_singh/dbguest.php");

$link = mysqli_connect($host, $user, $pass);
if (!$link) die("Couldn't connect to MySQL");


mysqli_select_db($link, $db)
	or die("Couldn't open $db: ".mysqli_error($link));


$query = mysqli_query($link,"SELECT Date FROM TRANSACTION WHERE Trxn_Number = '$trxn_number'");

if (!$query)
	 print("ERROR: ".mysqli_error($link));
else {
	if(mysqli_num_rows($query) == 0) 
		echo "<script>alert(\"No such transaction exists\")</script>";
	else{
	
	$output = mysqli_fetch_row($query); 
	
	foreach( $output as $date)
	{	
		
		$current =date('y-m-d',time());
		$your_date_time = strtotime($date);
		$your_date = date('y-m-d',$your_date_time);
		$days = round(abs(strtotime($current)-strtotime($your_date))/86400);
		
		if($days <= 30){
			$delete1 = mysqli_query($link, "DELETE FROM TRANSACION_DETAILS where Trxn_Number = '$trxn_number'");
	    		if (!$delete1) print("ERROR: ".mysqli_error($link));
				else {
    					$delete2 = mysqli_query($link, "DELETE FROM TRANSACTION where Trxn_Number = '$trxn_number'");
    					if (!$delete2) print("ERROR: ".mysqli_error($link));
    					else echo "<script>alert(\"Transaction cancelled successfully\")</script>";
					 }
		}
		else{
			echo "<script>alert(\"Transaction cannot be cancelled as it is $days days old\")</script>";
		}
	}
}
}


mysqli_close($link);

#print "<p><p>Connection closed. Bye...";
}
?>
<center>
<form name = "cancel transaction" action="" method="POST">
<b>Enter transaction number to be cancelled:</b>

<p>
Transaction Number<input type="text" name="trxn_number">
<p>

	
<input name="submit" type="submit" value="submit">
</form>



<p>

<a href="main.php"> back to MAIN menu</a>

</body>
</html>
