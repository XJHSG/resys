CKEDITOR.plugins.add('resource', {
icons: 'resource',
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
        editor.addCommand('insertResource', {
            exec: function showMyDialog(e) {
                var str = 'width=900,height=600,left=' + ((screen.width-900)/2) + ',top=' + ((screen.height-600)/2) + ',scrollbars=no,scrolling=no,location=no,toolbar=no'
                var w = window.open('File_Browse.aspx', 'MyWindow', str);                
            }
        });


        editor.ui.addButton('Resource', {
            label: 'Insert Resource',
            command: 'insertResource',
            toolbar: 'insert'
        });


    }
});