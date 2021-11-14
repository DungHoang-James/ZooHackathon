let btnSignup = document.getElementById('btnSignup');
let btnLogin = document.getElementById('btnLogin');
let btnReport = document.getElementById('btnReport');
let checkboxAuto = document.getElementById('checkboxAuto');
let signupForm = document.getElementById('signupForm');
let loginForm = document.getElementById('loginForm');
let btnSignupSubmit = document.getElementById('btnSignupSubmit');
let btnLoginSubmit = document.getElementById('btnLoginSubmit');
let email1 = document.getElementById('email1');
let email2 = document.getElementById('email2');
let password1 = document.getElementById('password1');
let password2 = document.getElementById('password2');
let password3 = document.getElementById('password3');
let authen = document.getElementById('authen');
let welcome = document.getElementById('welcome');

document.addEventListener('DOMContentLoaded', async function () {
	const currentUser = await chrome.storage.sync.get('currentUser');
	if (currentUser.currentUser) {
		authen.style.display = 'none';
		welcome.textContent = `Hello ${currentUser.currentUser.name}`;
		welcome.style.display = 'block';
	} else {
		authen.style.display = 'block';
		welcome.style.display = 'none';
	}

	const autoCheck = await chrome.storage.sync.get('autoCheck');
	checkboxAuto.checked = autoCheck.autoCheck ? true : false;
});

btnSignup.addEventListener('click', async () => {
	signupForm.style.display = signupForm.style.display === 'block' ? 'none' : 'block';
	if (signupForm.style.display === 'block') {
		loginForm.style.display = 'none';
	}
});

btnLogin.addEventListener('click', async () => {
	loginForm.style.display = loginForm.style.display === 'block' ? 'none' : 'block';
	if (loginForm.style.display === 'block') {
		signupForm.style.display = 'none';
	}
});

btnSignupSubmit.addEventListener('click', async (e) => {
	e.preventDefault();
	const email = email2.value;
	const password = password2.value;
	const passwordConfirm = password3.value;

	if (password === passwordConfirm) {
		// TODO: goi api signup

		const currentUser = { name: email.split('@')[0], count: 10 };
		await chrome.storage.sync.set({ currentUser });
		authen.style.display = 'none';
		welcome.textContent = `Hello ${currentUser.name}`;
		welcome.style.display = 'block';
		hideAuth();
	}
});

btnLoginSubmit.addEventListener('click', async (e) => {
	e.preventDefault();
	const email = email1.value;
	const password = password1.value;

	// TODO: goi api login

	const currentUser = { name: email.split('@')[0], count: 10 };
	await chrome.storage.sync.set({ currentUser });
	authen.style.display = 'none';
	welcome.textContent = `Hello ${currentUser.name}`;
	welcome.style.display = 'block';
	hideAuth();
});

btnReport.addEventListener('click', async () => {
	let [tab] = await chrome.tabs.query({ active: true, currentWindow: true });

	const deviceId = await chrome.storage.sync.get('deviceId');
	console.log(deviceId);
	
	chrome.scripting.executeScript({
		target: { tabId: tab.id },
		function: onReport,
	});
});

checkboxAuto.addEventListener('change', async (event) => {
	await chrome.storage.sync.set({ autoCheck: event.currentTarget.checked });
});

async function onReport() {
	

	// đi qua từng tag trên trình duyệt

	// TODO: gọi api POST report
	// alert('Report success!');
}

function hideAuth() {
	signupForm.style.display = 'none';
	loginForm.style.display = 'none';
	btnSignup.style.display = 'none';
	btnLogin.style.display = 'none';
}
