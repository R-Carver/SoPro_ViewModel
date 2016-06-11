//Adds DateTimePicker to HMTL5 unsupported browsers
if (!Modernizr.inputtypes.date) {
    $(function () {
        $("input[type=date]").datepicker({
            changeMonth: true,
            changeYear: true,
            yearRange: "-10:+50",
            //dateFormat: 'dd.mm.yy',
            firstDay: 1
        });
    });
}