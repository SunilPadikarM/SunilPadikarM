	<html>
	<head>
	<title>
	Add Customer
	</title>
	</head>
	<body>
	<center>
	<?php
	if(isset($_POST['submit']) || isset($_POST['Yes']) || isset($_POST['No'])){
	//It will get value of table which is passed in the textbox 
	$fname = htmlentities($_POST["fname"]);
	$lname = htmlentities($_POST["lname"]);
	$mail_address = htmlentities($_POST["mail_address"]);
	$mobile_number = htmlentities($_POST["mobile_number"]);


	require("/home/student_2018_fall/g_singh/dbguest.php");

	$link = mysqli_connect($host, $user, $pass);
	if (!$link) die("Couldn't connect to MySQL");
	//print "Successfully connected to server<p>";

	mysqli_select_db($link, $db)
		or die("Couldn't open $db: ".mysqli_error($link));
	//print "Successfully selected database \"$db\"<p>";



	#print(mysqli_num_rows($check));


	if(!(isset($_POST['Yes']) || isset($_POST['No']))){
		$check = mysqli_query($link, "SELECT * FROM CUSTOMER where C_Name = '$fname' and C_Last = '$lname'");
		if (!$check) print("ERROR: ".mysqli_error($link));
		if (mysqli_num_rows($check) != 0)
		{
			echo "<form action=\"\" method=\"POST\">A customer with the same name exists are you sure you want to add a new one ?
				<br> <input type=\"submit\" name=\"Yes\" value=\"yes\"> <input type=\"submit\" name=\"No\" value=\"no\"> 
				<input type=\"hidden\" name=\"fname\" value=\"".$fname."\">
				<input type=\"hidden\" name=\"lname\" value=\"".$lname."\">
	        	    	<input type=\"hidden\" name=\"mail_address\" value=\"".$mail_address."\">
	                	<input type=\"hidden\" name=\"mobile_number\" value=\"".$mobile_number."\"></form>";
				exit;
		}
		else
		{
			$result = mysqli_query($link, "INSERT into CUSTOMER (C_Name,C_Last,Phone,Mail_Addr)
			values ('$fname','$lname','$mobile_number','$mail_address')");
			if (!$result) print("ERROR: ".mysqli_error($link));
			else {
	    		print "<b>CUSTOMER ADDED</b>";
				 }

		}
	}
	elseif(isset($_POST['Yes']))
		{
			$result = mysqli_query($link, "INSERT into CUSTOMER (C_Name,C_Last,Phone,Mail_Addr)
			values ('$fname','$lname','$mobile_number','$mail_address')");

			if (!$result) print("ERROR: ".mysqli_error($link));
			else {
	    		print "<b>CUSTOMER ADDED</b><br><br><br>";
				}
		}
	else
		{
		print "<b>CUSTOMER NOT ADDED</b><br><br><br>";
		}


	mysqli_close($link);

	}
	?>


	<form action="" method="POST">
	<b>Enter Customer details:</b>

	<p>
	First Name<input type="text" name="fname">
	<p>
	Last Name<input type="text" name="lname">
	<p>
	Mail Address<input type="text" name="mail_address">
	<p>
	Mobile Number<input type="text" name="mobile_number">
	<p>

		
	<input name="submit" type="submit" value="Add Customer">
	</form>




	<p>

	<a href="main.php"> back to MAIN menu</a>

	</body>
	</html>
