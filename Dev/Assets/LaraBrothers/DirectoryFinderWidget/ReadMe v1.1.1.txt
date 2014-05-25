------------------------------------------------
        Directory Finder Widget for NGUI
                      by 
                 Lara Brothers
          (Roman Lara & Humberto Lara)
                  Version 1.1.1
---------------------------------------------------

Thank you for buy Directory Finder Widget for NGUI!

If you have a question, suggestions or comments, please
you write to my email: lara.ems.roman@live.com

-------------
 Description 
-------------

Directory Finder Widget is a widget created to NGUI. 
This is a file explorer which searches folders and files.

You can use to find certain files such as text, xml, 
audio, video, etc., and save the file path to load the 
same next time.

-----------------
 Version History
-----------------

v1.1.1 (2014-May-14 Wen : 2014-May-16 Fri)
    · Fixed:
        - Fixed an issue that made the selecting an item (file or folder) with keyboard or 
          with a joystick was not centered. (Caused by NGUI 3.5.9)

v1.1.0 (2014-Mar-24 Mon : 2014-Mar-24 Mon)
    · Added:
        - Compatibility with NGUI 3.5.5; This version prevents be backward compatible,
          because the UIButtonKeys component no longer used.
    · Fixed:
        - Fixed an issue that made the UIScrollView component constantly will be located.

v1.0.4 (2014-Feb-22 Sat : 2014-Feb-25 Tue)
    · Added:
        - Now supports versions NGUI 3.5.0 and 3.5.1
    · Fixed:
        - Issues caused by versions NGUI 3.5.0 and 3.5.1
            · Fixed an issue that made that after show a big list of files or directories, 
              the folder that has a list few files or directories will not be shown in 
              display area of ScrollView. Caused by version NGUI 3.5.0.
            · Fixed an issue caused by UIGrid that made the scroll bar will not work well.
              Caused by version NGUI 3.5.0.
              (Warning: If you prefer the version NGUI 3.4.9, it is recommended remove the 
              "UIScrollView.OnScrollBar" event of the "On Value Change" section that is in the 
              "UIScrollBar" component of the "Scroll Bar[Panel]" game object , because this 
              version of NGUI adds it automatically.)

v1.0.3 (2014-Jan-30 Thu : 2014-Feb-04 Tue)
    · Fixed:
        - A problem with obtaining the logical drives in linux.

v1.0.2 (2013-Dec-27 Fri : 2013-Dec-30 Mon)
    · Added:
        - It was ubdated to version NGUI 3.3.6 (previously 3.0.8 f7).
    · Fixed:
        - Usability was improved.

v1.0.1 (2013-Dec-19 Thu : 2013-Dec-19 Thu)
    · Fixed:
        - A bug for the Linux platform that gets the drives.

v1.0 (2013-Dec-08 Fri : 2013-Dec-13 Fri)
    · Version released.
    · Added:
        - It was updated to version NGUI 3.2.1 (previously 3.0.7 f1).
    · Fixed:
        - An error with the sliding of the elements 
          when the keyboard or game controller was used.
          It was caused by the new version NGUI 3.2.1 (previously 3.0.7 f1).
        - Descriptive text in English was corrected.

v1.0rc1 (2013-Dec-04 Wen : 2013-Dec-05 Thu)
    · Added:
        - It can browse with keyboard or game control.
        - It can use with a controller (DirectoryFinderController script).

v1.0b1 (2013-Nov-27 Wen : 2013-Dec-03 Tue)
    . Added:
        - It’s possible that the UI shown too files.
        - The items are ordered by sort, first it show 
          the folders and the files after.
    · Fixed:
        - The Directory Finder Widget has been improved.

v1.0a2 (2013-Nov-25 Mon : 2013-Nov-26 Tue)
    · Added:
        - Design of the UI.
        - Created the Directory Finder Widget UI.
        - UIItemF component.
        - UIDrive component.
        - UIDirectoryFinderButton component.
        - UIDirectoryFinder component.

v1.0a1 (2013-Nov-18 Mon : 2013–Nov-22 Fri)
    · Creation of the project.
    · Added:
        - DirectoryFinder script.
        - LogicalVolume script.
