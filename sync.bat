@echo off
set remote1=origin
set remote2=vsts

echo currently available remotes:
git remote -v
echo/
echo start sync:
echo fetch tags from %remote1%...
git fetch %remote1% --tags

echo fetch tags from %remote2%...
git fetch %remote2% --tags

echo push all to %remote1%...
git push %remote1% --all

echo push tags to %remote1%...
git push %remote1% --tags

echo push all to %remote2%...
git push %remote2% --all

echo push tags to %remote2%...
git push %remote2% --tags