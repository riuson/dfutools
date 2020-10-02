# DfuTools

This is a tools to manipulate STM32 DFU files.

## Using DfuToolCli

1. Create empty DFU

~~~ cmd
dfutoolcli dfu-create --set-device 0x1234 --set-product 0x5678 --set-vendor 0x0483 sample.dfu
~~~

2. Change IDs in DFU file

~~~ cmd
dfutoolcli dfu-change --set-device 0xf132 --set-product 0xf567 --set-vendor 0xf483 sample.dfu
~~~

3. Remove all targets from DFU file

~~~ cmd
dfutoolcli dfu-clear sample.dfu
~~~

4. Show DFU contents

~~~ cmd
dfutoolcli dfu-show --data-length 16 sample.dfu
~~~

5. Add new image element to specified target

~~~ cmd
dfutoolcli element-create --id 0 --set-address 0x08000000 --set-content element.bin sample.dfu
~~~

6. Extract image element from specified target to file

~~~ cmd
dfutoolcli element-extract --id 0 --index 0 --output-file element.bin sample.dfu
~~~

7. Remove image element from specified target

~~~ cmd
dfutoolcli element-remove --id 0 --index 0 sample.dfu
~~~

8. Change target's ID and Name

~~~ cmd
dfutoolcli target-change --id 0 --set-id 2 --set-name "Hello, World!" sample.dfu
~~~

9. Remove all image elements from target

~~~ cmd
dfutoolcli target-clear --id 0
~~~

10. Create new target

~~~ cmd
dfutoolcli target-create --set-name "Bootloader" --set-id 0 sample.dfu
~~~

11. Remove existing target

~~~ cmd
dfutoolcli target-remove --id 0 sample.dfu
~~~
                                                                            