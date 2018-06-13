function slideToggle(me) {
	$(me).next().slideToggle('slow');
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
				docElement.appendChild(createRow(data[i], ownerId));
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
						+ ' <button onclick="removeCorp(' + data.corporation_id + ', ' + ownerId + ')">x</button>'
						+ '</li>');
				}
				addCorpForRecruit(data.corporation_id, ownerId);
			})
			.fail(function () {
				alert('Cannot find a corporation called "' + corpName + '"');
			});
}

function addCorpForRecruit(corpId, ownerId) {
	var data = { CorporationId: corpId, RecruitId: ownerId };
	var url = window.location.protocol + "//" + window.location.host + '/';
	$.post(url + 'api/AuthoriseCorporation', data);
}

function removeCorp(corpId, ownerId) {	
	var data = { CorporationId: corpId, RecruitId: ownerId };
	var url = window.location.protocol + "//" + window.location.host + '/';
	$.ajax({
		url: url + 'api/AuthoriseCorporation',
		type: 'DELETE',
		success: function () {
			$('#' + corpId).remove();
		}
	});
}

function createRow(data, ownerId) {
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