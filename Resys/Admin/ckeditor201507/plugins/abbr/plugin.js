CKEDITOR.plugins.add('abbr', {
    icons: 'abbr',
    init: function (editor) {
        //Plugin logic goes here.
        /*
        editor.addCommand('insertTimestamp', {
            exec: function (editor) {
                var now = new Date();
                editor.insertHtml('The current date and time is: <em>' + now.toString() + '</em>');
            }
        });
        */
        editor.addCommand('abbr', new CKEDITOR.dialogCommand('abbrDialog'));
        editor.ui.addButton('Abbr', {
            label: 'Insert Abbreviation',
            command: 'abbr',
            toolbar: 'insert'
        });
        CKEDITOR.dialog.add('abbrDialog', this.path + 'dialogs/abbr.js');
    }
});