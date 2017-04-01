<?php
	//error_reporting(E_ALL ^ E_DEPRECATED);
	$hostname = "localhost";
	$user_name = "nowonbun";
	$password = "1234";
	$db_name = "travel";
	$conn = mysqli_connect($hostname,$user_name,$password,$db_name);
	
	$qy = "select * from test";
	$mysql_slt = mysqli_query($conn,$qy);
	while($row = mysqli_fetch_array($mysql_slt)){
		echo $row[0]."<br>";
	}
?>
