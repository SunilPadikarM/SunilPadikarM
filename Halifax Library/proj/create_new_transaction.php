	<html>
	<center>
	<head>
	<title>
	New Transaction
	</title>
	</head>
	<body>
		<?php
	require("../dbguest.php");
	if( isset($_POST['submit']) ){
	    //be sure to validate and clean your variables
	    $cus_number = htmlentities($_POST['customer_number']);
	    $mazine = $_POST['magazine'];

	    // Create connection
	    $conn = mysqli_connect($host, $user, $pass, $db);
	    // Check connection
	    if (!$conn) {
	        die("Connection failed: " . mysqli_connect_error());
	    }

	$result = mysqli_query($conn, "select TRANSACTION.CID, (sum(ITEM.Price * TRANSACION_DETAILS.Quantity * 1 - 2.5 * TRANSACTION.Discount_Code / 100)) as total  from TRANSACTION,TRANSACION_DETAILS,ITEM where ((Date between  DATE(NOW() - INTERVAL 5 YEAR) and NOW()) and TRANSACTION.Trxn_Number = TRANSACION_DETAILS.Trxn_Number and ITEM.ID = TRANSACION_DETAILS.ID and TRANSACTION.CID = $cus_number) Group by TRANSACTION.CID;");
	$total = 0;
	while ($b_row = mysqli_fetch_row($result)) {
			$total = $b_row[1];
		}
		$DC = 0;
		if($total > 500){
			$DC = 5;
		} else if ($total == 0){
			$DC = 0;
		} else if ($total > 100 && $total <= 200){
			$DC = 1;
		} else if ($total > 200 && $total <= 300){
			$DC = 2;
		} else if ($total > 300 && $total <= 400){
			$DC = 3;
		} else if ($total > 400 && $total <= 500){
			$DC = 4;
		}
		
		$res2 = mysqli_query($conn,"SELECT MAX(Trxn_Number) + 1 from TRANSACTION;");
		$ID = 0;
		while ($b_row = mysqli_fetch_row($res2)) {
			$ID = $b_row[0] + 1;
		}
		mysqli_query($conn,"INSERT INTO TRANSACTION VALUES ($ID, NOW(), $cus_number, $DC);");

		#$trnx_id = -1;
		#while ($b_row = mysqli_fetch_row($res1)) {
	#		$trnx_id = $b_row[0];
	#	}
	#	echo $trnx_id;
		foreach ($mazine as $option) {
			mysqli_query($conn, "INSERT INTO TRANSACION_DETAILS VALUES ($ID, $option.$value,1);");
			
		}
		echo "Transaction Added Successfully!";
	mysqli_close($conn);
	}
	?>
		<form action="" method="post">
	<b>New Transaction</b>
	<p>
	<br>
	<table>
		<tbody>
			<tr>
			<td>Customer Number-Name : </td>
			<td><?php
	require("../dbguest.php");

	$link = mysqli_connect($host, $user, $pass);
	if (!$link) die("Couldn't connect to MySQL");

	mysqli_select_db($link, $db)
		or die("Couldn't open $db: ".mysqli_error($link));

	$result = mysqli_query($link, "select CID, C_Name from CUSTOMER");

	if (!$result) print("ERROR: ".mysqli_error($link));
	else {
	    $num_rows = mysqli_num_rows($result);    
	}

	#Select Articles
	$result_Magaznie = mysqli_query($link, "select Maganize_ID, Title,Volume,PRICE from MAGAZINE, ITEM where MAGAZINE.Maganize_ID=ITEM.ID;");

	if (!$result) print("ERROR: ".mysqli_error($link));
	else {
	    $num_rows = mysqli_num_rows($result);
	}
	function printcustomers($result){
		print "<select name='customer_number'>\n";
		while ($a_row = mysqli_fetch_row($result)) {
			print "<option value=\"$a_row[0]\">$a_row[0]-$a_row[1]</option>";
		}
		print "</select>";
	}

	function printMagazine($result_Magaznie){
		print "<select name='magazine[]' multiple width:auto;>\n";
		while ($a_row = mysqli_fetch_row($result_Magaznie)) {
			print "<option value=\"$a_row[0]\">$a_row[1] - vol $a_row[2] - price  $a_row[3]</option>";
		}
		print "</select>";
	}
	printcustomers($result);
	print "</td><br><br><td>Magazine: </td><td>";
	printMagazine($result_Magaznie);
	print "</td>";
	mysqli_close($link);
	#print "<p><p>Connection closed. Bye...";
	?></tr>
			</tbody>
	</table>

	<input type="submit" name="submit">
	</form>
	<a href="main.php">click to go to main page</a>
	</body>
	</center>
	</html>
