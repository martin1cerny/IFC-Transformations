# IFC Transformations

This command line tool can be used to transform and anonymize IFC files. 

Available options:

```
  -v, --verbose                      (Default: false) Set output to verbose messages.

  -i, --input                        Required. Input IFC file

  -o, --output                       Required. Output IFC file

  -r, --reset-owning-application     (Default: true) Resets application in owner history

  --owning-application-developper    (Default: xBIM Team) Resets application in owner history

  --owning-application               (Default: xBIM Toolkit) Resets application in owner history

  --owning-application-id            (Default: XBIM) Resets application in owner history

  --owning-application-version       (Default: XBIM) Resets application in owner history

  -d, --dss-codes                    (Default: true) Sets DSS codes as properties based on IFC type

  -e, --editor-name                  (Default: Editor) Name of the editor of the file

  --editor-surname                   (Default: ) Name of the editor of the file

  --editor-organization              (Default: Ceská agentura pro standardizaci) Name of the editor of the file

  -a, --anonymize-elements           (Default: true) Set random names to all elements

  -c, --clear-all-properties         (Default: true) Deletes all properties and property sets before other processing

  -g, --reset-guids                  (Default: true) Creates new GUID for all IfcRoot entities

  --help                             Display this help screen.

  --version                          Display version information.
```