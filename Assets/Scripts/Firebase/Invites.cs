using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Invites : MonoBehaviour
{

    private bool canInitialize;
    public UnityEvent onInviteSend;
    public UnityEvent onInviteError;
    public UnityEvent onInviteCancel;

    public void Init()
    {
        canInitialize = true;
    }

    void HandleSentInvite(Task<Firebase.Invites.SendInviteResult> sendTask)
    {
        if (sendTask.IsCanceled)
        {
            onInviteCancel.Invoke();
        }
        else if (sendTask.IsFaulted)
        {
            onInviteError.Invoke();
        }
        else if (sendTask.IsCompleted)
        {
            onInviteSend.Invoke();
        }
    }

    public void SendInvite()
    {
        if (!canInitialize)
        {
            return;
        }

        var invite = new Firebase.Invites.Invite()
        {
            TitleText = "Challenge my High Score!",
            MessageText = "Play Pixel Soccer - FREE kicking game",
            CallToActionText = "PLAY THE GAME",
            EmailSubjectText = "Subjext",
            EmailContentHtml = "<a href='%%APPINVITE_LINK_PLACEHOLDER%%'><h1>Check it out here!</h1><img src='https://appjoy.org/wp-content/uploads/2016/06/firebase-invites-logo.png'></a>"
        };

        Firebase.Invites.FirebaseInvites
       .SendInviteAsync(invite).ContinueWith(HandleSentInvite);
    }

}
