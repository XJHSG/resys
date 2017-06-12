/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function(config) {
    //config.enterMode = CKEDITOR.ENTER_BR;
    /* 添加新的插件 */
    config.extraPlugins = 'timestamp,video';
    config.allowedContent = {
        $1: {
            // Use the ability to specify elements as an object.
            elements: CKEDITOR.dtd,
            attributes: true,
            styles: true,
            classes: true
        }
    };
    config.disallowedContent = 'script; *[on*]';
    
    
    /* 配置工具条 */
    config.toolbar = [
        /* { name: 'document', items: ['Save', 'NewPage', 'Preview', 'Print', '-', 'Templates'] }, */
		//{name: 'document', items: ['NewPage', 'Preview', 'Print', '-', 'Templates'] },
        { name: 'document', items: ['NewPage',  'Print', '-', 'Templates'] },
		{ name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
		{ name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Scayt'] },
		{ name: 'insert', items: ['Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
		'/',
		{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
		//{ name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
        { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl'] },
		//{ name: 'links', items: ['Link', 'Unlink', 'Anchor'] },	
        { name: 'links', items: ['Link', 'Unlink'] },
		'/',
		{ name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
		{ name: 'colors', items: ['TextColor', 'BGColor'] },
		//{ name: 'tools', items: ['Maximize', 'ShowBlocks', 'Timestamp',  '-', 'Source'] }
        //{ name: 'tools', items: ['Maximize', 'ShowBlocks', 'Timestamp',  'Video'] }
        { name: 'tools', items: ['Maximize', 'ShowBlocks', 'Timestamp', 'Video', '-', 'Source'] }
    ];
};
