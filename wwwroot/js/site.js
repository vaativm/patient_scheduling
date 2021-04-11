function formatDate(date) {
    var d = new Date(date),
      month = '' + (d.getMonth() + 1),
      day = '' + d.getDate(),
      year = d.getFullYear();

    if (month.length < 2)
      month = '0' + month;
    if (day.length < 2)
      day = '0' + day;

  return [year, month, day].join('-');
}

  function loadExpectedVisit(participantId) {
    var uri = '/Visits/LoadExpectedVisit';

    $.ajax({
      type: "GET",
      accepts: 'application/json',
      url: uri,
      contentType: 'application/json',
      data: {
        participantId: participantId
      },
      success: function (data) {
        document.getElementById("visit-date").value = formatDate(data.visitDate);

        if (data.nextAppointment)
          document.getElementById("next-appointment").value = formatDate(data.nextAppointment);

        document.getElementById("visit-setting-id").value = data.visitSettingId
        document.getElementById("date-participant-came").value = null;
        document.getElementById("participant-came").value = "";
      }
    });
}

function loadExpectedPSFVisit(participantId) {
  var uri = '/Visits/LoadExpectedPSFVisit';

  $.ajax({
    type: "GET",
    accepts: 'application/json',
    url: uri,
    contentType: 'application/json',
    data: {
      participantId: participantId
    },
    success: function (data) {
      document.getElementById("visit-date").value = formatDate(data.visitDate);

      if (data.nextAppointment)
        document.getElementById("next-appointment").value = formatDate(data.nextAppointment);

      document.getElementById("visit-setting-id").value = data.visitSettingId
      document.getElementById("date-participant-came").value = null;
      document.getElementById("participant-came").value = "";
    }
  });
}
 

  function getSchedule(participantId) {
    var uri = '/Participants/GetPatientSchedule';
    const tableHead = "<thead class='thead-light'>" +
      "<tr>" +
      "<th scope='col'>Time Point</th>" +
      "<th scope='col'>Window Opens</th>" +
      "<th scope='col'>Window Closes</th>" +
      "<th scope='col'>Assessment Completion Status</th>" +
      "<th scope='col'>Date Completed</th>" +
      "<th scope='col'>Reason</th>" +
      "</tr>" +
      +"</thead>"

    $.ajax
      ({
        type: "GET",
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: { participantId },
        success: function (data) {
          const table = $("#participant-schedule")
          const tableBody = $("#schedule");

          table.empty();
          tableBody.empty();
          table.append(tableHead);

          $.each(data, function (key, item) {
            const tr = "<tr>" +
              "<td>" + item.visitType + "</td>" +
              "<td>" + item.windowOpens + "</td>" +
              "<td>" + item.windowCloses + "</td>" +
              "<td>" + item.assessmentCompletion + "</td>" +
              "<td>" + item.completionDate + "</td>" +
              "<td>" + item.comment + "</td>" +
              "</tr>";
            tableBody.append(tr);
          });

          table.append(tableBody);
        },
        error: function (data) {
          alert(data.error);
        }
      });
}
function getPSFSchedule(participantId) {
  var uri = '/Participants/GetPSFSchedule';
  const tableHead = "<thead class='thead-light'>" +
    "<tr>" +
    "<th scope='col'>Time Point</th>" +
    "<th scope='col'>Expected Date</th>" +
    "<th scope='col'>Assessment Completion Status</th>" +
    "<th scope='col'>Date Completed</th>" +
    "<th scope='col'>Reason</th>" +
    "</tr>" +
    +"</thead>"

  $.ajax
    ({
      type: "GET",
      accepts: 'application/json',
      url: uri,
      contentType: 'application/json',
      data: { participantId },
      success: function (data) {
        const table = $("#participant-schedule")
        const tableBody = $("#schedule");

        table.empty();
        tableBody.empty();
        table.append(tableHead);

        $.each(data, function (key, item) {
          const tr = "<tr>" +
            "<td>" + item.visitStage + "</td>" +
            "<td>" + formatDate(item.expectedDate) + "</td>" +
            "<td>" + item.assessmentCompletion + "</td>" +
            "<td>" + item.completionDate + "</td>" +
            "<td>" + item.comment + "</td>" +
            "</tr>";
          tableBody.append(tr);
        });

        table.append(tableBody);
      },
      error: function (data) {
        alert(data.error);
      }
    });
}
