mergeInto(LibraryManager.library, {

  SendMessageToParent: function (str) {
      window.parent.postMessage(UTF8ToString(str), '*');
  },
  SendAlertToWindow: function(str){
        window.alert(UTF8ToString(str));
  }, 
  NotifyParentOfInit: function(){
      window.postMessage(UTF8ToString("Initialized"));
  }

});