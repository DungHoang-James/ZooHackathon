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

const BACKEND_URL = 'https://eaa9-2402-800-6341-eafc-149-87c5-8d38-5479.ngrok.io';

document.addEventListener('DOMContentLoaded', async function () {
	const currentUser = await chrome.storage.sync.get('currentUser');
	if (currentUser.currentUser) {
		authen.style.display = 'none';
		welcome.textContent = `Hello ${currentUser.currentUser.fullName}`;
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
		const res = await fetch(
			`${BACKEND_URL}/api/users/register?email=${email}&password=${password}&role=0`,
			{
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
			}
		);
		const currentUser = await res.json();
		await chrome.storage.sync.set({ currentUser });
		authen.style.display = 'none';
		welcome.textContent = `Hello ${currentUser.fullName}`;
		welcome.style.display = 'block';
		hideAuth();
	}
});

btnLoginSubmit.addEventListener('click', async (e) => {
	e.preventDefault();
	const email = email1.value;
	const password = password1.value;

	const res = await fetch(`${BACKEND_URL}/api/users/login?email=${email}&password=${password}`, {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json',
		},
	});
	const currentUser = await res.json();
	await chrome.storage.sync.set({ currentUser });
	authen.style.display = 'none';
	welcome.textContent = `Hello ${currentUser.fullName}`;
	welcome.style.display = 'block';
	hideAuth();
});

btnReport.addEventListener('click', async () => {
	let [tab] = await chrome.tabs.query({ active: true, currentWindow: true });

	const currentUser = await chrome.storage.sync.get('currentUser');
	const deviceId = (await chrome.storage.sync.get('deviceId')).deviceId;


	console.log(currentUser);
	chrome.scripting.executeScript({
		target: { tabId: tab.id },
		function: onReport,
		args: [
			BACKEND_URL,
			currentUser && currentUser.currentUser ? currentUser.currentUser.id : null,
			currentUser && currentUser.currentUser ? null : deviceId,
		],
	});
});

checkboxAuto.addEventListener('change', async (event) => {
	await chrome.storage.sync.set({ autoCheck: event.currentTarget.checked });
});

function hideAuth() {
	signupForm.style.display = 'none';
	loginForm.style.display = 'none';
	btnSignup.style.display = 'none';
	btnLogin.style.display = 'none';
}

async function onReport(url, userID, deviceID) {
	let leaf = [];
	let texts = [];
	let images = [];

	async function scan() {
		function iterator(node) {
			if (node.children.length) {
				for (var i = 0; i < node.children.length; i++) {
					iterator(node.children[i]);
				}
			} else {
				leaf.push(node);
			}
		}

		iterator(document.body);

		let i = 0;

		const dictionary = [
			'bán',
			'buôn bán',
			'voi',
			'hổ',
			'gấu',
			'tê tê',
			'culi',
			'rái cá',
			'trăn',
			'rắn',
			'cự đà',
			'rùa',
			'sừng',
			'tê giác',
			'ngà',
			'ngà voi',
			'mật',
			'vảy',
			'xương',
			'súng săn',
			'bẫy',
			'html page',
			'new',
		];

		const labels = ['rhinoceros', 'elephant', 'tiger'];

		function santinizeWord(word) {
			return word.toLowerCase().split('-').join('').split('.').join('').split('_').join('');
		}

		function validateText(textContent, node) {
			const words = textContent.split(' ');
			words.forEach((word, index) => {
				let oriWord1 = word;
				let oriWord2 = word + ' ' + words[index + 1] || '';
				const word1 = santinizeWord(word);
				const word2 = word1 + ' ' + santinizeWord(words[index + 1] || '');
				if (dictionary.includes(word2.trim())) {
					node.innerHTML =
						words.slice(0, index).join(' ') +
						` <span style='color:Tomato;text-transform:uppercase'>${oriWord2}</span> ` +
						words.slice(index + 2).join(' ');
					texts.push({ text: word2 });
				} else if (dictionary.includes(word1.trim())) {
					node.innerHTML =
						words.slice(0, index).join(' ') +
						` <span style='color:Tomato;text-transform:uppercase'>${oriWord1}</span> ` +
						words.slice(index + 1).join(' ');
					texts.push({ text: word1 });
				}
			});
		}

		function timeout(ms) {
			return new Promise((resolve) => setTimeout(resolve, ms));
		}

		async function myLoop(node) {
			if (leaf[i - 1]) {
				if (!leaf[i - 1].imageCorrect) {
					leaf[i - 1].style.border = 'initial';
				}
			}
			node.style.border = '2px solid red';

			if (leaf[i].tagName === 'IMG') {
				const imageURL = leaf[i].src;
				const res = await fetch(`${url}/api/reports/detectImage?imagePath=${imageURL}`, {
					method: 'GET',
					headers: {
						'Content-Type': 'application/json',
					},
				});

				const data = await res.json();

				if (data.length) {
					const image = data.find((image) =>
						labels.some((el) => image.label.toLowerCase().includes(el))
					);
					if (image) {
						node.style.border = '2px solid red';
						node.imageCorrect = true;
						images.push({ imageURL, percentCorrect: Math.round(image.probability * 100) });
					}
				}
			} else {
				const textContent = node.textContent;
				validateText(textContent, leaf[i]);
			}

			i++;

			if (i < leaf.length) {
				await timeout(500);
				await myLoop(leaf[i]);
			}
		}

		await myLoop(leaf[i]);
	}

	await scan();

	console.log(images);
	console.log(texts);

	const avgPercent = images.reduce((acc, cur) => acc + cur.percentCorrect, 0) / images.length;

	console.log(avgPercent);
	if (avgPercent && avgPercent > 75) {
		const raw = JSON.stringify({
			userID,
			deviceID,
			reportImages: images,
			reportTexts: Array.from(new Set(texts.map((el) => el.text.trim()))).map((el) => ({
				text: el,
			})),
		});

		await fetch(`${url}/api/reports`, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
			},
			body: raw,
		});

		alert('Report successful');
	} else {
		alert('Report fail. Accuracy percent is not more than 75%');
	}
}
