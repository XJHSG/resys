CKEDITOR.plugins.add('timestamp', {
    icons: 'timestamp',
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
        editor.addCommand('insertTimestamp', {
            exec: function showMyDialog(e) {
            var str = 'width=980,height=650,left=' + ((screen.width - 900) / 2) + ',top=' + ((screen.height - 650) / 2) + ',scrollbars=no,scrolling=no,location=no,toolbar=no'
                var w = window.open('File_Browse2.aspx', 'MyWindow', str);                
            }
        });
        

        editor.ui.addButton('Timestamp', {
            label: '插入资源',
            command: 'insertTimestamp',
            toolbar: 'insert'
        });


    }
});