CKEDITOR.dialog.add( 'abbrDialog', function ( editor ) {
    return {
        title: 'Abbreviation Properties',
        minWidth: 900,
        minHeight: 600,

        contents: [
            {
                id: 'tab-basic',
                label: 'Basic Settings',
                elements: [
                    {
                        type: 'text',
                        id: 'abbr',
                        label: 'Abbreviation',
                        validate: CKEDITOR.dialog.validate.notEmpty("Abbreviation field cannot be empty.")
                    },
                    {
                        type: 'text',
                        id: 'title',
                        label: 'Explanation',
                        validate: CKEDITOR.dialog.validate.notEmpty("Explanation field cannot be empty.")
                    }
                ]
            },
            {
                id: 'tab-adv',
                label: 'Advanced Settings',
                elements: [
                    {
                        type: 'text',
                        id: 'id',
                        label: 'Id'
                    }
                ]
            }
        ],
        onOk: function () { editor.insertHtml('<h2>This is a sample header</h2><p>This is a sample paragraph.</p>');  }
    };
});