using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command {

    public const int MaxElements = 4;
    
    public const string Bypass = "bypass"; //Makes an attempt at bypassing a machine's firewall
    public const string Focus = "focus"; //Sets the FocusTab's content to whatever's name is passed after this command
    public const string Proxy = "proxy"; //Makes an attempt at avoiding all-out detection by the network
    public const string Cat = "cat"; //This command is used to display the content of the selected file
    public const string CD = "cd"; //This command is used to navigate folders (change directories)
    public const string Clear = "clear"; //This command is used to clear the TerminalTab of all content
    public const string SSH = "ssh"; //This command is used to connect to a machine on the network
    public const string Exit = "exit"; //This command is used to end an SSH connection
    public const string EXE = "exe"; //This command shows those programs (even those which are hidden) which are available to be executed
    public const string Help = "help"; //This command is used to display the most basic commands available to the player (the query tab holds more detailed information)
    public const string Kill = "kill"; //This command is used to terminate a process using that process' PID. Note that some terminations require passwords; the password may be used as an optional parameter to terminate the process in one line, or if the player attempts to terminate a password-protected process without adding this parameter, the player is subsequently prompted for said password
    public const string Login = "login"; //This command is usedd to gain admin access to the connected computer via password entry
    public const string MV = "mv"; //This command is used to move or rename a file
    public const string Probe = "probe"; //This command scans the ports that are open and also shows if there is a firewall and a proxy system present
    public const string PS = "ps"; //This command displays the current processes that are running, along with those processes' associated IDs (PIDs)
    public const string Replace = "replace"; //This command replaces the contents of the open or specified text file with that specified by the player
    public const string Reboot = "reboot"; //This command reboots the connected computer
    public const string RM = "rm"; //This command is used to delete files within a directory, though it cannot delete directories
    public const string Save = "save"; //This command saves the player's progress
    public const string SCP = "scp"; //This command downloads the selected file to the player's machine. By default, files downloaded in this manner are stored in the ~/home directory, but an optional destination parameter may be used to sspecify a different destination for the download
    public const string Upload = "upload"; //This command attempts to upload a file from the player's computer to another one in the network
}
