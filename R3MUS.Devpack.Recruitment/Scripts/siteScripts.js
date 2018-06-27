function slideToggle(me) {
	$('#' + me).slideToggle('slow');
}
function showModal(me) {
	$('#' + me).modal();
}

function getMailHeaders() {
	var ownerId = $.trim($('#character-id').text());
	var url = window.location.protocol + "//" + window.location.host + '/';
	$.get(url + 'api/GetMail/' + ownerId)
		.done(function (data) {
			var docElement = document.createDocumentFragment();
			for (var i = 0; i < data.length; i++) {
				docElement.appendChild(createMailRow(data[i], ownerId));
			}
			$('#mail-headers').append(docElement);
			$('#mail-headers').slideDown();
			$('#loading-mail').slideUp();
		})
		.fail(function () {
			var docElement = document.createDocumentFragment();
			var row = document.createElement('tr');
			var td = document.createElement('td');
			td.innerText = 'Something went wrong.';
			$(row).append(td);
			$(docElement).append(row);
			$('#mail-headers').append(docElement);
			$('#mail-headers').slideDown();
			$('#loading-mail').slideUp();
		});
}

function corpSearch(ownerId) {
	var corpName = $('#corpName').val();
		var url = window.location.protocol + "//" + window.location.host + '/';
		$.get(url + 'api/SearchCorporation?corporationName=' + corpName)
			.done(function (data) {
				if ($('#approvedCorporations').html().indexOf(data.corporation_id) == -1) {
					$('#approvedCorporations').append('<li id="' + data.corporation_id + '">' + data.corporation_name
						+ '<span id="status-' + data.corporation_id + '"></span><button onclick="removeCorp(' + data.corporation_id + ', ' + ownerId + ')">x</button>'
						+ '</li>');
				}
				addCorpForRecruit(data.corporation_id, ownerId);
			})
			.fail(function () {
				alert('Cannot find a corporation called "' + corpName + '"');
			});
}

function changeStatus(corpId, ownerId) {
	var data = { CorporationId: corpId, RecruitId: ownerId, Status: $('#available-statuses').val() };
	var url = window.location.protocol + "//" + window.location.host + '/';
	$.post(url + 'api/ChangeApplicantStatus', data)
		.success(function (data) {
			$('#status-change-message').text('Status Changed');
		})
		.fail(function () {
			$('status-change-message').text('Status Failed To Change');
		});
}

function addCorpForRecruit(corpId, ownerId) {
	var data = { CorporationId: corpId, RecruitId: ownerId };
	var url = window.location.protocol + "//" + window.location.host + '/';
	$.post(url + 'api/AuthoriseCorporation', data)
		.success(function (data) {
			$('#status-' + corpId).text(': ' + data);
		})
		.fail(function () {
			$('#status-' + corpId).text(': Failed to add this corporation.');
		});
}

function removeCorp(corpId, ownerId) {	
	var data = { CorporationId: corpId, RecruitId: ownerId };
	var url = window.location.protocol + "//" + window.location.host + '/';
	$.ajax({
		url: url + 'api/AuthoriseCorporation',
		type: 'DELETE',
		data: data,
		success: function () {
			$('#' + corpId).remove();
		}
	});
}

function createMailRow(data, ownerId) {
	var row = document.createElement('tr');
	$(row).addClass('mouse-change');
	row.onclick = function () { displayMail(data.Id) };

	var td = document.createElement('td');
	td.id = 'mail-InOrOut-' + data.Id;
	td.innerText = data.InOrOut;
	row.appendChild(td);

	td = document.createElement('td');
	td.id = 'mail-Sender-' + data.Id;
	td.innerText = data.Sender;
	row.appendChild(td);

	td = document.createElement('td');
	td.id = 'mail-RecipientsLine-' + data.Id;
	td.innerText = data.RecipientsLine;
	row.appendChild(td);

	td = document.createElement('td');
	td.id = 'mail-Subject-' + data.Id;
	td.innerText = data.Subject;
	row.appendChild(td);

	td = document.createElement('td');
	td.id = 'mail-TimeStamp-' + data.Id;
	td.innerText = data.TimeStamp;
	row.appendChild(td);

	return row;
}

function displayMail(mailId) {
	$('#loading-mail').slideDown();
	var model = {
		Id: mailId,
		OwnerId: $.trim($('#character-id').text()),
		InOrOut: $.trim($('#mail-InOrOut-' + mailId).text()),
		Sender: $.trim($('#mail-Sender-' + mailId).text()),
		RecipientsLine: $.trim($('#mail-RecipientsLine-' + mailId).text()),
		Subject: $.trim($('#mail-Subject-' + mailId).text()),
		TimeStamp: $.trim($('#mail-TimeStamp-' + mailId).text())
	};
	var url = window.location.protocol + "//" + window.location.host + '/';
	var htmlStart = '<html><head><link href="' + url + 'Content/bootstrap.css" rel="stylesheet"></head><body>';
	var htmlEnd = '</body></html>';

	$.post(url + 'api/GetMail', model)
		.done(function (data) {
			var subject = '<div class="row"><div class="col-sm-2">Subject: </div><div class="col-sm-10">' + data.Subject + '</div></div>';
			var sender = '<div class="row"><div class="col-sm-2">Sender: </div><div class="col-sm-10">' + data.Sender + '</div></div>';
			var recipients = '<div class="row"><div class="col-sm-2">Recipients: </div><div class="col-sm-10">' + data.RecipientsLine + '</div></div>';
			var timestamp = '<div class="row"><div class="col-sm-2">Timestamp: </div><div class="col-sm-10">' + data.TimeStamp + '</div></div>';
			var body = '<div class="row"><div class="col-sm-12">' + data.Body + '</div></div>';

			$('#loading-mail').slideUp();

			var w = window.open();
			$(w.document.body).html(htmlStart + subject + sender + recipients + timestamp + '<div class="row">&nbsp;</div>' + body + htmlEnd);
		})
		.fail(function () {
			var w = window.open();
			$(w.document.body).html(htmlStart + '<h1>Unable to display mail.</h1>' + htmlEnd);
		});
}

function getContacts() {
	var ownerId = $.trim($('#character-id').text());
	var url = window.location.protocol + "//" + window.location.host + '/';
	$.get(url + 'api/GetContacts/' + ownerId)
		.done(function (data) {
			var docElement = document.createDocumentFragment();
			for (var i = 0; i < data.length; i++) {
				docElement.appendChild(createContactElement(data[i], ownerId));
			}
			$('#contact-modal-dialog').append(docElement);
			$('#contact-modal-dialog').slideDown();
			$('#loading-contacts').slideUp();
		})
		.fail(function () {
			var docElement = document.createDocumentFragment();
			var div = document.createElement('div');
			$(div).html('Something went wrong.');
			$(docElement).append(div);
			$('#contact-modal-dialog').append(docElement);
			$('#contact-modal-dialog').slideDown();
			$('#loading-contacts').slideUp();
		});
}

function createContactElement(data) {
	var parentDiv = document.createElement('div');
	$(parentDiv).attr('class', 'col-md-3 modal-segment alert ' + data.AlertStyle);

	var div = document.createElement('div');
	$(div).attr('class', 'row');

	var strong = document.createElement('strong');
	$(strong).html('Name: ');

	div.appendChild(strong);
	$(div).html($(div).html() + data.Name);
	parentDiv.appendChild(div);

	div = document.createElement('div');
	$(div).attr('class', 'row');

	strong = document.createElement('strong');
	$(strong).html('Contact Type: ');

	div.appendChild(strong);
	$(div).html($(div).html() + data.contact_type);
	parentDiv.appendChild(div);

	div = document.createElement('div');
	$(div).attr('class', 'row');

	strong = document.createElement('strong');
	$(strong).html('Standing: ');

	div.appendChild(strong);
	$(div).html($(div).html() + data.standing.toFixed(2));
	parentDiv.appendChild(div);
	
	div = document.createElement('div');
	$(div).attr('class', 'row');
	if (data.contact_type == 'character') {
		var a = document.createElement('a');
		$(a).attr('target', '_blank');
		$(a).attr('href', 'https://zkillboard.com/character/' + data.contact_id + '/');
		$(a).text('zKillboard');
		div.appendChild(a);

		var blank = document.createElement('span');
		$(blank).html('&nbsp;')
		div.appendChild(blank);

		a = document.createElement('a');
		$(a).attr('target', '_blank');
		$(a).attr('href', 'https://evewho.com/pilot/' + data.Name.replace(' ', '+') + '/');
		$(a).text('Evewho');
		div.appendChild(a);
	}
	parentDiv.appendChild(div);

	return parentDiv;
}

function getWalletJournal() {
	var ownerId = $.trim($('#character-id').text());
	var url = window.location.protocol + "//" + window.location.host + '/';
	$.get(url + 'api/GetWalletJournal/' + ownerId)
		.done(function (data) {
			var docElement = document.createDocumentFragment();
			for (var i = 0; i < data.length; i++) {
				docElement.appendChild(createJournalElement(data[i], ownerId));
			}
			$('#journal-modal-dialog').append(docElement);
			$('#journal-modal-dialog').slideDown();
			$('#loading-journal').slideUp();
		})
		.fail(function () {
			var docElement = document.createDocumentFragment();
			var div = document.createElement('div');
			$(div).html('Something went wrong.');
			$(docElement).append(div);
			$('#journal-modal-dialog').append(docElement);
			$('#journal-modal-dialog').slideDown();
			$('#loading-journal').slideUp();
		});
}
function getWalletTransactions() {
	var ownerId = $.trim($('#character-id').text());
	var url = window.location.protocol + "//" + window.location.host + '/';
	$.get(url + 'api/GetWalletTransactions/' + ownerId)
		.done(function (data) {
			var docElement = document.createDocumentFragment();
			for (var i = 0; i < data.length; i++) {
				docElement.appendChild(createTransactionElement(data[i], ownerId));
			}
			$('#transaction-modal-dialog').append(docElement);
			$('#transaction-modal-dialog').slideDown();
			$('#loading-transactions').slideUp();
		})
		.fail(function () {
			var docElement = document.createDocumentFragment();
			var div = document.createElement('div');
			$(div).html('Something went wrong.');
			$(docElement).append(div);
			$('#transaction-modal-dialog').append(docElement);
			$('#transaction-modal-dialog').slideDown();
			$('#loading-transactions').slideUp();
		});
}

function createJournalElement(data) {
	var row = document.createElement('tr');

	var td = document.createElement('td');
	td.innerText = data.Timestamp;
	row.appendChild(td);

	td = document.createElement('td');
	td.innerText = data.Amount.toLocaleString(undefined, { maximumFractionDigits: 2 });
	row.appendChild(td);

	td = document.createElement('td');
	td.innerText = data.Balance.toLocaleString(undefined, { maximumFractionDigits: 2 });
	row.appendChild(td);

	td = document.createElement('td');
	td.innerText = data.Description;
	row.appendChild(td);

	td = document.createElement('td');
	td.innerText = data.Reason;
	row.appendChild(td);

	return row;
}

function createTransactionElement(data) {
	var row = document.createElement('tr');

	var td = document.createElement('td');
	td.innerText = data.Timestamp;
	row.appendChild(td);

	td = document.createElement('td');
	td.innerText = data.ClientName;
	row.appendChild(td);

	td = document.createElement('td');
	td.innerText = data.BuySell;
	row.appendChild(td);

	td = document.createElement('td');
	td.innerText = data.Quantity;
	row.appendChild(td);

	td = document.createElement('td');
	td.innerText = data.ItemTypeName;
	row.appendChild(td);

	td = document.createElement('td');
	td.innerText = data.UnitPrice.toLocaleString(undefined, { maximumFractionDigits: 2 });
	row.appendChild(td);

	return row;
}
