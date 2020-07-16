const baseUrl = "https://localhost:44321"
const transactionApi = "/transaction";
const clientApi = "/clientapi";
const accountApi = "/accountapi";
let accountId;


// Account API
function getAccountDetails() {
	const Http = new XMLHttpRequest();
	accountId = document.querySelector("#enter-account-number").value || 0;
	if (accountId == 0) {
		alert('You must type a valid account number!');
	} else {
		const url = baseUrl + accountApi + '/' + accountId;
		Http.open("GET", url);
		Http.send();
		let accountInfo;
		Http.onload = () => {
			console.warn('inside onload')
			if (Http.status != 200) {
				alert('error');

			} else {
				accountInfo = JSON.parse(Http.responseText);
				document.querySelector('#acc-no').innerHTML = accountInfo["accountNumber"];
				document.querySelector('#acc-type').innerHTML = accountInfo["accountType"];
				document.querySelector('#acc-owner').innerHTML = (accountInfo["accountClient"])["firstName"] + " " + (accountInfo["accountClient"])["lastName"]
				document.querySelector('#acc-balance').innerHTML = accountInfo["balance"];

				document.querySelector('#account-details').classList.remove('hidden');
			}
		}
	}
}

function createAccount() {
	const Http = new XMLHttpRequest();
	let type = document.querySelector('#enter-ac-type').value;
	let ownerId = parseInt(document.querySelector('#enter-ac-owner').value || 0);

	if (type == '' || ownerId == 0) {
		alert('Please fill the name fields');
	} else {

		clearAccountModal();
		let data = {
			"AccountType": type,
			"Balance": 0,
			"AccountClientId": ownerId
		};

		Http.open("POST", baseUrl + accountApi);
		Http.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		Http.send(JSON.stringify(data));

		Http.onload = () => {
			if (Http.status != 200) {
				alert('post error');
			} else {
				alert(`Account created!`);
			}
		}
	}
}

function clearAccountModal() {
	document.querySelector('#enter-ac-type').value = '';
	document.querySelector('#enter-ac-owner').value = '';
}

function deleteAccount() {
	let acId = document.querySelector("#enter-account-number").value || 0;
	let confirmDelete = confirm('Are you sure you want to delete account number ' + acId);

	if (acId && acId != 0 && confirmDelete) {
		const Http = new XMLHttpRequest();
		const url = baseUrl + accountApi + "/" + acId;

		Http.open("DELETE", url);
		Http.send();

		Http.onload = () => {
			console.warn('inside onload')
			if (Http.status != 200) {
				alert('error');
			}
			else {
				alert("Account "+acId+ " deleted!");
			}
		}
	}
}



// TransactionAPI
function makeTransfer() {
	const Http = new XMLHttpRequest();
	let sendingAccount = parseInt(document.querySelector("#sending-account").value) || 0;
	let receivingAccount = parseInt(document.querySelector("#receiving-account").value) || 0;
	let amountToTransfer = parseFloat(document.querySelector('#amount-transfer').value) || 0;

	if (sendingAccount === 0 || receivingAccount === 0 || amountToTransfer === 0) {
		alert('please fill all fields with valid inputs');
	} else {
		let data = {
			"sendingAccountId": sendingAccount,
			"receivingAccountId": receivingAccount,
			"amountToTransfer": amountToTransfer
		};
		console.log(data);
		Http.open("POST", baseUrl + transactionApi);
		Http.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		Http.send(JSON.stringify(data));
		Http.onload = () => {
			console.warn('inside load of post request');
			if (Http.status != 200) {
				alert('post error');
			} else {
				alert(`Transfered ${amountToTransfer} successfully`);
			}
		}

	}

}

function deposit() {
	const Http = new XMLHttpRequest();
	let account = parseInt(document.querySelector('#account-to-deposit').value) || 0;
	let amount = parseFloat(document.querySelector('#amount-deposit').value) || 0;

	if (account === 0 || amount === 0) {
		alert('Please fill fields with valid inputs');
	} else {
		let data = {
			"accountId": account,
			"amount": amount
		};

		Http.open("POST", baseUrl + transactionApi + "/deposit");
		Http.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		Http.send(JSON.stringify(data));

		Http.onload = () => {
			if (Http.status != 200) {
				alert('post error');
			} else {
				alert(`Deposited ${amount} successfully`);
			}
		}
	}
}

function draw() {
	const Http = new XMLHttpRequest();
	let account = parseInt(document.querySelector('#account-to-draw').value) || 0;
	let amount = parseFloat(document.querySelector('#amount-draw').value) || 0;

	if (account === 0 || amount === 0) {
		alert('Please fill fields with valid inputs');
	} else {
		let data = {
			"accountId": account,
			"amount": amount
		};

		Http.open("POST", baseUrl + transactionApi + "/draw");
		Http.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		Http.send(JSON.stringify(data));

		Http.onload = () => {
			if (Http.status != 200) {
				alert('post error');
			} else {
				alert(`Draw ${amount} successfully`);
			}
		}
	}
}

// CLient Methods
function createClient() {
	const Http = new XMLHttpRequest();
	let fname = document.querySelector('#enter-first-name').value;
	let lname = document.querySelector('#enter-last-name').value;
	let city = document.querySelector('#enter-city').value;

	if (fname == '' || lname == '') {
		alert('Please fill the name fields');
	} else {

		clearClientModal();
		let data = {
			"LastName": fname,
			"FirstName": lname,
			"City": city
		};

		Http.open("POST", baseUrl + clientApi);
		Http.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		Http.send(JSON.stringify(data));

		Http.onload = () => {
			if (Http.status != 200) {
				alert('post error');
			} else {
				alert(`Client created!`);
			}
		}
	}
}

function deleteClient() {
	let clientId = document.querySelector("#enter-client-number").value || 0;
	let confirmDelete = confirm('Are you sure you want to delete client number ' + clientId);

	if (clientId && clientId != 0 && confirmDelete) {
		const Http = new XMLHttpRequest();
		const url = baseUrl + clientApi + "/" + clientId;

		Http.open("DELETE", url);
		Http.send();

		Http.onload = () => {
			console.warn('inside onload')
			if (Http.status != 200) {
				alert('error');
			}
			else {
				alert("Client "+clientId+ " deleted!");
			}
		}
	}
}


function getClientDetails() {
	const Http = new XMLHttpRequest();
	accountId = document.querySelector("#enter-client-number").value || 0;
	if (accountId == 0) {
		alert('You must type a valid client number!');
	} else {
		const url = baseUrl + clientApi + "/" + accountId;
		Http.open("GET", url);
		Http.send();
		let clientInfo;
		Http.onload = () => {
			console.warn('inside onload')
			if (Http.status != 200) {
				alert('error');

			} else {
				clientInfo = JSON.parse(Http.responseText);
				document.querySelector('#client-no').innerHTML = clientInfo["clientId"];
				document.querySelector('#client-fname').innerHTML = clientInfo["firstName"];
				document.querySelector('#client-lname').innerHTML = clientInfo["lastName"];
				document.querySelector('#client-city').innerHTML = clientInfo["city"];

				document.querySelector('#client-details').classList.remove('hidden');
			}
		}
	}
}

function clearClientModal() {
	document.querySelector('#enter-first-name').value = '';
	document.querySelector('#enter-last-name').value = '';
	document.querySelector('#enter-city').value = '';
}




